using CED.Application.Services.Authentication.Commands.Register;
using CED.Application.Services.Authentication.Queries.Login;
using CED.Contracts.Entities.Subject;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CED.WebAPI.Controllers;

[Route("[controller]")]
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

    [HttpPost]
    [Route("CreateSubject")]
    public async Task<IActionResult> CreateSubject(CreateUpdateSubjectDto createUpdateSubjectDto)
    {
        var command = _mapper.Map<CreateUpdateSubjectCommand>(createUpdateSubjectDto);

        var result = await _mediator.Send(command);

        return Ok(result);
    }
    [HttpPost]
    [Route("UpdateSubject")]
    public async Task<IActionResult> UpdateSubject(CreateUpdateSubjectDto createUpdateSubjectDto)
    {
        var command = _mapper.Map<CreateUpdateSubjectCommand>(createUpdateSubjectDto);

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    [HttpDelete]
    [Route("DeleteSubject/{id:guid}")]
    public async Task<IActionResult> DeleteSubject(Guid id)
    {
        var command = _mapper.Map<DeleteSubjectCommand>(id);

        var result = await _mediator.Send(command);

        return Ok(result);
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
        SubjectDto subject = await _mediator.Send(query);

        return Ok(subject);
    }
}

