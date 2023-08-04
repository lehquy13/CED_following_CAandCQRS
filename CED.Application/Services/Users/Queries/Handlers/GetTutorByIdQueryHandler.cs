using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Contracts.TutorReview;
using CED.Contracts.Users;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Review;
using CED.Domain.Subjects;
using CED.Domain.Users;
using FluentResults;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.Handlers;

public class GetTutorByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<TutorDto>, TutorDto>
{
    private readonly IUserRepository _userRepository;
    private readonly ITutorRepository _tutorRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IClassInformationRepository _classInformationRepository;

    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationInfoRepository;
    private readonly IRepository<TutorReview> _tutorReviewRepository;


    public GetTutorByIdQueryHandler(IUserRepository userRepository,
        ITutorRepository tutorRepository,
        ISubjectRepository subjectRepository,
        IRepository<TutorVerificationInfo> tutorVerificationInfoRepository,
        IRepository<TutorMajor> tutorMajorRepository, IMapper mapper, IRepository<TutorReview> tutorReviewRepository, IClassInformationRepository classInformationRepository) : base(mapper)
    {
        _userRepository = userRepository;
        _tutorRepository = tutorRepository;
        _subjectRepository = subjectRepository;
        _tutorVerificationInfoRepository = tutorVerificationInfoRepository;
        _tutorMajorRepository = tutorMajorRepository;
        _tutorReviewRepository = tutorReviewRepository;
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<Result<TutorDto>> Handle(GetObjectQuery<TutorDto> query, CancellationToken cancellationToken)
    {
        try
        {
            var tutor = await _tutorRepository.GetById(query.ObjectId);
            if ( tutor is null)
            {
                return Result.Fail(new NonExistTutorError());
            }

            TutorDto result = _mapper.Map<TutorDto>(tutor);

            var teachingClasses =
                _classInformationRepository.GetAll().Where(x => x.TutorId.Equals(query.ObjectId)).ToList();
            var reviews =  (await _tutorReviewRepository.GetAllList()).Join(
                teachingClasses,
                rev => rev.ClassInformationId,
                cl => cl.Id,
                (rev,cl) => new
                {
                    review = rev,
                    tutorId = cl.TutorId,
                    learnerID = cl.LearnerId,
                    
                    
                }).ToList();
            result.TutorReviewDtos  = PaginatedList<TutorReviewDto>.CreateAsync(
                reviews.Join(
                    await _userRepository.GetAllList(),
                    rev => rev.learnerID,
                    user => user.Id,
                    (rev, learner) => new TutorReviewDto()
                    {
                        LearnerName = learner.FirstName + learner.LastName,
                        LearnerId = learner.Id,
                        TutorId = rev.tutorId??Guid.Empty,
                        Description = rev.review.Description,
                        Rate = rev.review.Rate,
                      
                        CreationTime = rev.review.CreationTime,
                        LastModificationTime = rev.review.LastModificationTime,
                    }
                )
                ,query.PageIndex,query.PageSize,reviews.Count
            );
          
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}