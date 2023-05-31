using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;
using CED.Contracts;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using CED.Web.CustomerSide.Utilities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CED.Web.CustomerSide.Controllers;
[Route("[controller]")]
public class ClassInformationController : Controller
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
    [Route("")]
    public async Task<IActionResult> Index(int pageIndex = 1, string subjectName = "")
    {
        var query = new GetAllClassInformationsQuery()
        {
            PageSize = _pageSize,
            PageIndex = pageIndex,
            SubjectName = subjectName
        };
        var classInformation = await _mediator.Send(query);
        var obj1 = PaginatedList<ClassInformationDto>.CreateAsync(classInformation, pageIndex, _pageSize);
        return View(classInformation);
    }
    
    // GET api/<ClassInformationController>/5
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Detail(Guid id)
    {
        var query = new GetObjectQuery<ClassInformationDto>();
        var classInformation = await _mediator.Send(query);

        return View(classInformation);
    }

    // POST api/<ClassInformationController>
    [Authorize]
    [HttpPost]
    [Route("CreateClassInformation")]
    public async Task<IActionResult> CreateClassInformation(CreateUpdateClassInformationDto createUpdateClassInformationDto)
    {
        var command = _mapper.Map<CreateUpdateClassInformationCommand>(createUpdateClassInformationDto);

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    // PUT api/<ClassInformationController>/5
    [Authorize]
    [HttpPost]
    [Route("UpdateClassInformation")]
    public async Task<IActionResult> UpdateClassInformation(CreateUpdateClassInformationDto createUpdateClassInformationDto)
    {
        var command = _mapper.Map<CreateUpdateClassInformationCommand>(createUpdateClassInformationDto);

        var result = await _mediator.Send(command);

        return Ok(result);
    }

    // DELETE api/<ClassInformationController>/5
    
    [Authorize]
    [HttpPost]
    [Route("RequestGettingClass")]
    public async Task<IActionResult> RequestGettingClass(RequestGettingClassRequest requestGettingClassRequest)
    {
        var command = _mapper.Map<RequestGettingClassCommand>(requestGettingClassRequest);
        var result = await _mediator.Send(command);
        
        return Ok(result);
    }

   
}
