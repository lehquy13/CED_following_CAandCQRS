using CED.Application.Services.Subjects.Commands;
using CED.Application.Services.Subjects.Queries;
using CED.Contracts.Subjects;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CED.WebAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SubjectController : ControllerBase
{

    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public SubjectController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

   
    // Query
    [HttpGet]
    [Route("GetAllSubjects")]

    public async Task<IActionResult> GetAllSubjects(/*should be a hót isd*/)
    {
        var query = new GetAllSubjectsQuery();
        List<SubjectDto> subjects = await _mediator.Send(query);
        
        return Ok(subjects);
    }

    [HttpGet]
    [Route("GetSubject/{id}")]
    public async Task<IActionResult> GetSubject(Guid id)
    {
        var query = _mapper.Map<GetSubjectQuery>(id);
        var subject = await _mediator.Send(query);

        return Ok(subject.Value);
    }
}

