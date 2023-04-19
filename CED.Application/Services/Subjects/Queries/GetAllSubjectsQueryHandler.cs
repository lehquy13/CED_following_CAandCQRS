using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Queries;
using CED.Contracts.ClassInformations;
using CED.Contracts.Subjects;
using CED.Domain.Subjects;
using MapsterMapper;
using MediatR;

namespace CED.Application.Services.Subjects.Queries;

public class GetAllSubjectsQueryHandler : GetAllQueryHandler<GetAllSubjectsQuery, SubjectDto>
{
    private readonly ISubjectRepository _subjectRepository;
    public GetAllSubjectsQueryHandler(ISubjectRepository subjectRepository, IMapper mapper):base(mapper)
    {
        _subjectRepository = subjectRepository;
    }
    public override async Task<List<SubjectDto>> Handle(GetAllSubjectsQuery query, CancellationToken cancellationToken)
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

