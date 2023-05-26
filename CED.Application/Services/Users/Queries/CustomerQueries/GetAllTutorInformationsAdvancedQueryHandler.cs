using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.CustomerQueries;

public class GetAllTutorInformationsAdvancedQueryHandler : GetAllQueryHandler<GetAllTutorInformationsAdvancedQuery, TutorDto>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;

    public GetAllTutorInformationsAdvancedQueryHandler(
        ISubjectRepository subjectRepository,
        IUserRepository userRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;
        _userRepository = userRepository;
        _tutorMajorRepository = tutorMajorRepository;
    }

    public override async Task<List<TutorDto>> Handle(GetAllTutorInformationsAdvancedQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var subjects = _subjectRepository.GetAll();
            var tutors = _userRepository.GetTutors().AsEnumerable();

            var tutorsMajors = _tutorMajorRepository.GetAll()
                .GroupBy(t => t.TutorId)
                .Select(major => new
                {
                    tutorId = major.Key,
                    majorId = major.ToList()
                });

            if (query.Academic != AcademicLevel.Optional)
            {
                tutors = tutors.Where(user => user.AcademicLevel == query.Academic);
            }

            if (!string.IsNullOrEmpty(query.Address))
            {
                tutors = tutors.Where(user => user.Address.Contains(query.Address));
            }

            if (query.Gender != Gender.None)
            {
                tutors = tutors.Where(user => user.Gender == query.Gender);
            }

            if (query.BirthYear != 0)
            {
                tutors = tutors.Where(user => user.BirthYear == query.BirthYear);
            }

            var tutorDtos = _mapper.Map<List<TutorDto>>(tutors);

            foreach (var t in tutorDtos)
            {
                var objectMajor = tutorsMajors.FirstOrDefault(x => x.tutorId.Equals(t.Id));
                if (objectMajor != null)
                {
                    foreach (var majorId in objectMajor.majorId)
                    {
                        var sub = subjects.FirstOrDefault(x => x.Id.Equals(majorId));
                        if (sub is not null)
                        {
                            t.Majors.Add(_mapper.Map<SubjectDto>(sub));
                        }
                    }
                }
            }
            var result = _mapper.Map<List<TutorDto>>(
                tutors.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize)
                    .ToList()
            );
            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}