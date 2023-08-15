using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries.GetAllClassInformationsQuery;
using CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CED.WebAPI.Controllers;

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
    public async Task<IActionResult> GetAllClassInformations(int pageIndex, string subjectName = "")
    {
        var query = new GetAllClassInformationsQuery()
        {
            PageSize = _pageSize,
            PageIndex = pageIndex,
            SubjectName = subjectName
        };
        var classInformation = await _mediator.Send(query);
        if (classInformation.IsFailed)
        {
            return BadRequest("Get classes failed");
        }
        return Ok(classInformation);
    }
    
    // GET api/<ClassInformationController>/5
    [HttpGet]
    [Route("GetClassInformation/{id}")]
    public async Task<IActionResult> GetClassInformation(Guid id)
    {
        var query = new GetObjectQuery<ClassInformationForDetailDto>
        {
            ObjectId = id
        };
        var classInformation = await _mediator.Send(query);
        if(classInformation.IsFailed)
        {
            return BadRequest("Get class's information with id: "+ id +" failed");
        }
        return Ok(classInformation);
    }

    // POST api/<ClassInformationController>
    [HttpPost]
    [Route("CreateClassInformation")]
    public async Task<IActionResult> CreateClassInformation(CreateClassInformationByCustomer createUpdateClassInformationDto)
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
    [Authorize(Policy = "RequireTutorRole")]
    [HttpPut]
    [Route("RequestGettingClass")]
    public async Task<IActionResult> RequestGettingClass(RequestGettingClassRequest requestGettingClassRequest)
    {
        var command = _mapper.Map<RequestGettingClassCommand>(requestGettingClassRequest);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

   
}
