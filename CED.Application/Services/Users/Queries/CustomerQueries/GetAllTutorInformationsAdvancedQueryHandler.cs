using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.CustomerQueries;
/// <summary>
/// Todo: Upgrade this query
/// </summary>
public class
    GetAllTutorInformationsAdvancedQueryHandler : GetAllQueryHandler<GetAllTutorInformationsAdvancedQuery, TutorDto>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly ITutorRepository _tutorRepository;
    private readonly IUserRepository _userepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;

    public GetAllTutorInformationsAdvancedQueryHandler(
        ISubjectRepository subjectRepository,
        IUserRepository userepository,
        ITutorRepository tutorRepository,
        IRepository<TutorMajor> tutorMajorRepository,
        IMapper mapper) : base(mapper)
    {
        _subjectRepository = subjectRepository;
        _tutorRepository = tutorRepository;
        _tutorMajorRepository = tutorMajorRepository;
        _userepository = userepository;
    }

    public override async Task<PaginatedList<TutorDto>> Handle(GetAllTutorInformationsAdvancedQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var subjects = await _subjectRepository.GetAllList();
            var tutors = _tutorRepository.GetAll().AsEnumerable();
            var tutorAsUser = _userepository.GetTutors().AsEnumerable();


            if (query.Academic != AcademicLevel.Optional)
            {
                tutors = tutors.Where(user => user.AcademicLevel == query.Academic);
            }

            if (!string.IsNullOrEmpty(query.Address))
            {
                tutorAsUser = tutorAsUser.Where(user => user.Address.Contains(query.Address));
            }

            if (query.Gender != Gender.None)
            {
                tutorAsUser = tutorAsUser.Where(user => user.Gender == query.Gender);
            }

            if (query.BirthYear != 0)
            {
                tutorAsUser = tutorAsUser.Where(user => user.BirthYear == query.BirthYear);
            }
            tutors = tutors.ToList();

            var tutorsMajors = await _tutorMajorRepository.GetAllList();

            if (!string.IsNullOrEmpty(query.SubjectName))
            {
                var newsubjects = subjects.Where(s => s.Name.ToLower().Contains(query.SubjectName.ToLower()));
               
                tutorsMajors = tutorsMajors.Where(x =>  newsubjects.Select(ns => ns.Id).Contains(x.SubjectId)).ToList();
                tutors = tutors.Where(x =>  tutorsMajors.Select(tM => tM.TutorId).Contains(x.Id));
                
            }

            var mergeList = tutors.Join(
                tutorAsUser,
                tutor => tutor.Id,
                user => user.Id,
                (tutor, user) => (user,tutor).Adapt<TutorDto>()
            );
       
           
            var totalPages = tutors.Count();
            

            var result = _mapper.Map<List<TutorDto>>(
                mergeList.Skip((query.PageIndex - 1) * query.PageSize).Take(query.PageSize)
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