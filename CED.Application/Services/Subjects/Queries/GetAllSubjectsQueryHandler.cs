using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Subjects;
using CED.Domain.Subjects;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Queries;

public class GetAllSubjectsQueryHandler : GetAllQueryHandler<GetObjectQuery<List<SubjectDto>>, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    public GetAllSubjectsQueryHandler(ISubjectRepository subjectRepository, IMapper mapper):base(mapper)
    {
        _subjectRepository = subjectRepository;
    }
    public override async Task<List<SubjectDto>> Handle(GetObjectQuery<List<SubjectDto>> query, CancellationToken cancellationToken)
    {
        try
        {
            var subjects = await _subjectRepository.GetAllList();

            return _mapper.Map<List<SubjectDto>>(subjects);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

