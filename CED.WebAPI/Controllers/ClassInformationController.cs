using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CED.WebAPI.Controllers;
[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ClassInformationController : ControllerBase
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly int _pageSize = 10;

    public ClassInformationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // Query
    // GET: api/<ClassInformationController>
    [HttpGet]
    [Route("GetAllClassInformations")]
    public async Task<IActionResult> GetAllClassInformations(int pageIndex, string subjectName)
    {
        var query = new GetAllClassInformationsQuery()
        {
            PageSize = _pageSize,
            PageIndex = pageIndex,
            SubjectName = subjectName
        };
        var classInformation = await _mediator.Send(query);

        return Ok(classInformation);
    }
    
    // GET api/<ClassInformationController>/5
    [HttpGet]
    [Route("GetClassInformation/{id}")]
    public async Task<IActionResult> GetClassInformation(Guid id)
    {
        var query = new GetObjectQuery<ClassInformationDto>();
        var classInformation = await _mediator.Send(query);

        return Ok(classInformation);
    }

    // POST api/<ClassInformationController>
    [HttpPost]
    [Route("CreateClassInformation")]
    public async Task<IActionResult> CreateClassInformation(CreateUpdateClassInformationDto createUpdateClassInformationDto)
    {
        var command = _mapper.Map<CreateUpdateClassInformationCommand>(createUpdateClassInformationDto);

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    // PUT api/<ClassInformationController>/5
    [HttpPut]
    [Route("UpdateClassInformation")]
    public async Task<IActionResult> UpdateClassInformation(CreateUpdateClassInformationDto createUpdateClassInformationDto)
    {
        var command = _mapper.Map<CreateUpdateClassInformationCommand>(createUpdateClassInformationDto);

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    // DELETE api/<ClassInformationController>/5
    
    
    [HttpPut]
    [Route("RequestGettingClass")]
    public async Task<IActionResult> RequestGettingClass(RequestGettingClassRequest requestGettingClassRequest)
    {
        var command = _mapper.Map<RequestGettingClassCommand>(requestGettingClassRequest);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

   
}
