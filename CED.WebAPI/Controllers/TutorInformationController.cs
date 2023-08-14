
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Queries.GetClassInformation;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Application.Services.Users.Tutor.Registers;
using CED.Contracts.ClassInformations;
using CED.Contracts.Users;
using CED.Contracts.Users.Tutors;
using CED.Domain.Shared.ClassInformationConsts;
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
    // GET: api/<TutorInformationController>
    [HttpGet]
    [Route("GetAllTutors")]
    public async Task<IActionResult> GetAllTutors(int pageIndex, string subjectName )
    {
        var query = new GetAllTutorInformationsAdvancedQuery()
        {
            PageIndex = pageIndex,
            PageSize = _pageSize
        };
        var tutorDtos = await _mediator.Send(query);
        if(tutorDtos.IsSuccess)
            return Ok(tutorDtos);
        return BadRequest(tutorDtos.Errors);
    }
    
    // GET api/<TutorInformationController>/5
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Detail(Guid id)
    {
        var query = new GetObjectQuery<TutorForDetailDto>()
        {
            ObjectId = id
        };
        var tutorDto = await _mediator.Send(query);
        if(tutorDto.IsSuccess)
            return Ok(tutorDto.Value);
        return BadRequest(tutorDto.Errors);
    }
    
    // POST api/<TutorInformationController>/TutorRegistration
    [Authorize]
    [HttpPost]
    [Route("TutorRegistration")]
    public async Task<IActionResult> TutorRegistration(TutorForRegistrationDto tutorForRegistrationDto)
    {
        //TODO: need to change Subjects to SubjectIds, Verification
        var command = new TutorRegistrationCommand(tutorForRegistrationDto);

        var result = await _mediator.Send(command);

        
        if (result.IsSuccess)
        {
            return NoContent(); 
        }

        return BadRequest(result.Errors);
    }
    

   

   
}
