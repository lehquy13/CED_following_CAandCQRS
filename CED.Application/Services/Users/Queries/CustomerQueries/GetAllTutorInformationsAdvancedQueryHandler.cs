using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Contracts.Users.Tutors;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using FluentResults;
using Mapster;
using MapsterMapper;

namespace CED.Application.Services.Users.Queries.CustomerQueries;
/// <summary>
/// Todo: Upgrade this query
/// </summary>
public class
    GetAllTutorInformationsAdvancedQueryHandler : GetAllQueryHandler<GetAllTutorInformationsAdvancedQuery, TutorForListDto>
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

    public override async Task<Result<PaginatedList<TutorForListDto>>> Handle(GetAllTutorInformationsAdvancedQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var subjects = await _subjectRepository.GetAllList();
            var tutors =  _tutorRepository.GetAll();

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
               
                tutors = tutors.Where(x => x.Subjects.Any(y => y.Name.Contains(query.SubjectName)));
                
            }
            var tutorFromDb = tutors.ToList();
            var mergeList = _mapper.Map<List<TutorForListDto>>(tutorFromDb);
           
            var totalPages = tutorFromDb.Count;

            var result =
                PaginatedList<TutorForListDto>.CreateAsync(mergeList, query.PageIndex, query.PageSize, totalPages);

            return result;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}