using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.CustomerQueries;

public class
    GetAllTutorInformationsAdvancedQueryHandler : GetAllQueryHandler<GetAllTutorInformationsAdvancedQuery, TutorDto>
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

    public override async Task<PaginatedList<TutorDto>> Handle(GetAllTutorInformationsAdvancedQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var subjects = await _subjectRepository.GetAllList();
            var tutors = _userRepository.GetTutors().AsEnumerable();
            var tutorsMajors = _tutorMajorRepository.GetAll();


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

            if (!string.IsNullOrEmpty(query.SubjectName))
            {
                var newsubjects = subjects.Where(s => s.Name.ToLower().Contains(query.SubjectName.ToLower()));
               
                tutorsMajors = tutorsMajors.Where(x =>  newsubjects.Select(ns => ns.Id).Contains(x.SubjectId));
                tutors = tutors.Where(x =>  tutorsMajors.Select(tM => tM.TutorId).Contains(x.Id));
                
            }


            var tutorsMajorsResult = tutorsMajors
                .GroupBy(t => t.TutorId)
                .ToList()
                .Select(major => new
                {
                    tutorId = major.Key,
                    majorId = major.ToList()
                })
                .ToList();

            var enumerable = tutors as User[] ?? tutors.ToArray();
            var totalPages = enumerable.Count();

            var tutorDtos = _mapper.Map<List<TutorDto>>(tutors);

            foreach (var t in tutorDtos)
            {
                var objectMajor = tutorsMajorsResult.FirstOrDefault(x => x.tutorId.Equals(t.Id));
                if (objectMajor != null)
                {
                    foreach (var id in objectMajor.majorId)
                    {
                        var sub = subjects.FirstOrDefault(x => x.Id.ToString().Equals(id.ToString()));
                        if (sub is not null)
                        {
                            t.Majors.Add(_mapper.Map<SubjectDto>(sub));
                        }
                    }
                }
            }

            var result = _mapper.Map<List<TutorDto>>(
                enumerable.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize)
                    .ToList()
            );

            var result1 =
                PaginatedList<TutorDto>.CreateAsync(result, query.PageIndex, query.PageSize, totalPages);

            return result1;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}