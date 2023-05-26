using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Domain.Repository;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Queries;

public class GetAllSubjectsQueryHandler : GetAllQueryHandler<GetObjectQuery<List<SubjectDto>>, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    public GetAllSubjectsQueryHandler(ISubjectRepository subjectRepository,IRepository<TutorMajor> tutorMajorRepository, IMapper mapper):base(mapper)
    {
        _subjectRepository = subjectRepository;
        _tutorMajorRepository = tutorMajorRepository;
    }
    public override async Task<List<SubjectDto>> Handle(GetObjectQuery<List<SubjectDto>> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var subjects =  _subjectRepository.GetAll();

            if (query.Guid != Guid.Empty)
            {
                var tutorMajors = _tutorMajorRepository.GetAll().Where(x => x.TutorId == query.Guid)
                    .Select(x => x.SubjectId);
                subjects = subjects.Where(x => !tutorMajors.Contains(x.Id));
            }

            return _mapper.Map<List<SubjectDto>>(subjects);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

