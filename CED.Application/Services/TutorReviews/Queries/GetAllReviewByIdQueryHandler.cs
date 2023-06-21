using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.TutorReview;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Review;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.TutorReviews.Queries;

public class GetAllReviewByIdQueryHandler : GetAllQueryHandler<GetAllReviewByIdQuery, TutorReviewDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IRepository<TutorReview> _tutorReviewRepository;

    public GetAllReviewByIdQueryHandler(IUserRepository userRepository,IRepository<TutorReview> tutorReviewRepository, IMapper mapper, IClassInformationRepository classInformationRepository):base(mapper)
    {
        _userRepository = userRepository;
        _tutorReviewRepository = tutorReviewRepository;
        _classInformationRepository = classInformationRepository;
    }
    public override async Task<PaginatedList<TutorReviewDto>> Handle(GetAllReviewByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var teachingClasses =
                _classInformationRepository.GetAll().Where(x => x.TutorId.Equals(query.Guid)).ToList();
            var reviews =  _tutorReviewRepository.GetAll().Join(
                    teachingClasses,
                    rev => rev.ClassInformationId,
                    cl => cl.Id,
                    (rev,cl) => new
                    {
                        review = rev,
                        tutorId = cl.TutorId,
                        learnerID = cl.LearnerId
                    }).ToList();
            
            var results  = reviews.Join(
                await _userRepository.GetAllList(),
                rev => rev.learnerID,
                user => user.Id,
                (rev, learner) => new TutorReviewDto()
                {
                    LearnerName = learner.FirstName + learner.LastName,
                    LearnerId = learner.Id,
                    TutorId = rev.tutorId??Guid.Empty,
                    Description = rev.review.Description,
                    Rate = rev.review.Rate
                }
            );
           
            
            //testing mapping paginatedlist 
            return PaginatedList<TutorReviewDto>.CreateAsync(
                    results,query.PageIndex,query.PageSize,reviews.Count
                );
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

