
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Application.Services.Users.Tutor.Commands.ApplyClass;
using CED.Contracts.ClassInformations;
using CED.Contracts.Users;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CED.WebAPI.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class TutorInformationController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly int _pageSize = 10;

    public TutorInformationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // Query
    // GET: api/<ClassInformationController>
    [HttpGet]
    [Route("GetAllClassInformations")]

    public async Task<IActionResult> GetAllTutor(int pageIndex, string subjectName )
    {
        var query = new GetAllTutorInformationsAdvancedQuery()
        {
            PageIndex = pageIndex,
            PageSize = _pageSize
        };
        var tutorDtos = await _mediator.Send(query);
        return Ok(tutorDtos);
    }
    
    // GET api/<ClassInformationController>/5
    [HttpGet]
    [Route("GetClassInformation/{id}")]
    public async Task<IActionResult> GetClassInformation(Guid id)
    {
        var query = _mapper.Map<GetClassInformationQuery>(id);
        var classInformation = await _mediator.Send(query);
        
        return Ok(classInformation);
    }

   

   
}
