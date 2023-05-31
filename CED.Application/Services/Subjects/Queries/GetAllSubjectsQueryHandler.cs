using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Queries;

public class GetAllSubjectsQueryHandler : GetAllQueryHandler<GetObjectQuery<PaginatedList<SubjectDto>>, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;

    public GetAllSubjectsQueryHandler(ISubjectRepository subjectRepository,IRepository<TutorMajor> tutorMajorRepository, IMapper mapper):base(mapper)
    {
        _subjectRepository = subjectRepository;
        _tutorMajorRepository = tutorMajorRepository;
    }
    public override async Task<PaginatedList<SubjectDto>> Handle(GetObjectQuery<PaginatedList<SubjectDto>> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var subjects =  _subjectRepository.GetAll();
            var totalSubjects = subjects.Count();
            if (query.Guid != Guid.Empty)
            {
                var tutorMajors = _tutorMajorRepository.GetAll().Where(x => x.TutorId == query.Guid)
                    .Select(x => x.SubjectId);
                subjects = subjects.Where(x => !tutorMajors.Contains(x.Id));
            }
            
            //testing mapping paginatedlist 
            return PaginatedList<SubjectDto>.CreateAsync(_mapper.Map<List<SubjectDto>>(subjects.ToList()),query.PageIndex,query.PageSize,totalSubjects);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

