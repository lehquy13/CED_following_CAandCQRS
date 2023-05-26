﻿using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.Users.Tutor.Commands.ApplyClass;
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

    public ClassInformationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    // Query
    // GET: api/<ClassInformationController>
    [HttpGet]
    [Route("GetAllClassInformations")]

    public async Task<IActionResult> GetAllClassInformations()
    {
        var query = new GetAllClassInformationsQuery();
        List<ClassInformationDto> classInformation = await _mediator.Send(query);


        return Ok(classInformation);
    }
    
    // GET api/<ClassInformationController>/5
    [HttpGet]
    [Route("GetClassInformation/{id}")]
    public async Task<IActionResult> GetClassInformation(Guid id)
    {
        var query = _mapper.Map<GetClassInformationQuery>(id);
        ClassInformationDto classInformation = await _mediator.Send(query);

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
