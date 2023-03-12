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
    public async Task<IActionResult> CreateSubjectAsync(CreateUpdateSubjectDto createUpdateSubjectDto)
    {
        var command = _mapper.Map<CreateSubjectCommand>(createUpdateSubjectDto);

        var result = await _mediator.Send(command);

        return Ok(result);
    }
    [HttpGet]
    [Route("GetAllSubjects")]

    public async Task<IActionResult> GetAllSubjectsAsync(/*should be a hót isd*/)
    {
        var query = new GetSubjectsQuery();
        List<SubjectDto> menus = await _mediator.Send(query);
       

        return Ok(menus);
    }
}

