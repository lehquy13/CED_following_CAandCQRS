using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Queries;
using CED.Contracts.ClassInformations;
using CED.Domain.Subjects;
using MapsterMapper;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CED.Application.Services.Subjects.Queries;

public class GetAllSubjectsLookUpQueryHandler : GetAllQueryHandler<GetAllSubjectsLookUpQuery, SubjectLookupDto>
{
    private readonly ISubjectRepository _subjectRepository;
    public GetAllSubjectsLookUpQueryHandler(ISubjectRepository subjectRepository, IMapper mapper):base(mapper)
    {
        _subjectRepository = subjectRepository;
    }
    public override async Task<List<SubjectLookupDto>> Handle(GetAllSubjectsLookUpQuery query, CancellationToken cancellationToken)
    {
        try
        {
            var subjects = await _subjectRepository.GetAllList();
            var subjectDtos = _mapper.Map<List<SubjectLookupDto>>(subjects);


            var serializes = JsonConvert.SerializeObject(subjectDtos);
            query.HttpContext.Session.SetString("SubjectList", serializes);

            //return Enumerable.Empty<SubjectLookupDto>().ToList();
            return subjectDtos;
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

