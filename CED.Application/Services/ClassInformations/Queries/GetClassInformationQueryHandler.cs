using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.TutorReview;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Review;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Queries;

public class
    GetClassInformationQueryHandler : GetByIdQueryHandler<GetObjectQuery<ClassInformationDto>, ClassInformationDto>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRepository<RequestGettingClass> _requestGettingClassRepositoryepository;
    private readonly IRepository<TutorReview> _reviewRepository;

    public GetClassInformationQueryHandler(IClassInformationRepository classInformationRepository,
        IRepository<RequestGettingClass> requestGettingClassRepositoryepository,
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IMapper mapper, IRepository<TutorReview> reviewRepository) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;
        _requestGettingClassRepositoryepository = requestGettingClassRepositoryepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
        _reviewRepository = reviewRepository;
    }

    public override async Task<ClassInformationDto?> Handle(GetObjectQuery<ClassInformationDto> query,
        CancellationToken cancellationToken)
    {
        var classInformation = await _classInformationRepository.GetById(query.Guid);

        if (classInformation == null)
        {
            return null;
        }

        var subject = await _subjectRepository.GetById(classInformation.SubjectId);
        var tutors = await _userRepository.GetAllList();

        var requests = (await _requestGettingClassRepositoryepository.GetAllList())
            .Where(x => x.ClassInformationId.Equals(classInformation.Id))
            .GroupJoin(
                tutors,
                req => req.TutorId,
                tu => tu.Id,
                (req, tu) => (req, tu.FirstOrDefault()).Adapt<RequestGettingClassMinimalDto>()
            ).ToList();
        
        var classDto = (classInformation, subject, requests).Adapt<ClassInformationDto>();
        //handle review
        var review = _reviewRepository.GetAll().FirstOrDefault(x => x.ClassInformationId.Equals(classInformation.Id));
        if (review is not null)
        {
            classDto.TutorReviewDto = _mapper.Map<TutorReviewDto>(review);
        }
        //handle tutor info
        if (classInformation.TutorId is not null  )
        {
            var tutor = tutors.SingleOrDefault(x => x.Id == (Guid)classInformation.TutorId);
            if (tutor is not null)
            {
                classDto.TutorName = tutor.FirstName +" " + tutor.LastName;
                classDto.TutorEmail = tutor.Email;
                classDto.TutorPhoneNumber = tutor.PhoneNumber;
                classDto.TutorDtoId = tutor.Id;
            } 
           
        }

        if (classInformation.LearnerId is not null)
        {
            var learner = tutors.SingleOrDefault(x => x.Id == (Guid)classInformation.LearnerId);

            if (learner is not null)
                classDto.LearnerName = learner.FirstName + " " + learner.LastName;
        }
    

        //handle class's review


        return classDto;
    }
}