using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Users;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Student.Queries;

public class GetLearnerByIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<LearnerDto>, LearnerDto?>
{
    private readonly IClassInformationRepository _classInformationRepository;

    private readonly IUserRepository _userRepository;
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;

    public GetLearnerByIdQueryHandler(IUserRepository userRepository,
        IClassInformationRepository classInformationRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper, ISubjectRepository subjectRepository) : base(mapper)
    {
        _classInformationRepository = classInformationRepository;

        _tutorMajorRepository = tutorMajorRepository;
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
    }

    public override async Task<LearnerDto?> Handle(GetObjectQuery<LearnerDto> query,
        CancellationToken cancellationToken)
    {
        try
        {
            User? user = await _userRepository.GetById(query.Guid);
            if (user is null)
            {
                return null;
            }

            var classInfors = _classInformationRepository.GetLearningClassInformationsByUserId(user.Id);
            var subjects = await _subjectRepository.GetAllList();
            
            var learner = _mapper.Map<LearnerDto>(user);
            learner.LearningClassInformations = PaginatedList<ClassInformationDto>.CreateAsync(
                _mapper.Map<List<ClassInformationDto>>(classInfors),
                1,
                100
            );
            foreach (var i in learner.LearningClassInformations)
            {
                i.SubjectName = subjects.First(x => x.Id.Equals(i.SubjectId)).Name;
            }
            return learner;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}