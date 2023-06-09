using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Contracts;
using CED.Contracts.ClassInformations;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Shared;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Web.CustomerSide.Models;
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

    private async Task PackStaticListToView()
    {
        // ViewData["Roles"] = EnumProvider.Roles;
        // ViewData["Genders"] = EnumProvider.FullGendersOption;
        // ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
        // ViewData["LearningModes"] = EnumProvider.LearningModes;
        // ViewData["Statuses"] = EnumProvider.Status;


        ViewData["Subjects"] = await _mediator.Send(new GetObjectQuery<PaginatedList<SubjectDto>>());
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
            SubjectName = subjectName,
            Status = Status.Available
        };
        var classInformations = await _mediator.Send(query);
        if (!string.IsNullOrWhiteSpace(subjectName))
        {
            ViewBag.SubjectSearch = subjectName;
        }

        return View(classInformations);
    }

    // GET api/<ClassInformationController>/5
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Detail(Guid id)
    {
        if (id.Equals( Guid.Empty))
        {
            return NotFound();
        }
        var query = new GetObjectQuery<ClassInformationDto>()
        {
            Guid = id
        };
        var classInformation = await _mediator.Send(query);
        var query1 = new GetAllClassInformationsQuery()
        {
            PageSize = _pageSize,
            PageIndex = 1,
            SubjectName = classInformation.SubjectName
        };
        var classInformations = await _mediator.Send(query1);
        
        return View(
            new ClassInformationDetailViewModel()
        {
            ClassInformationDto = classInformation,
            RelatedClasses = classInformations
        });
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        await PackStaticListToView();
        //await PackStudentAndTuTorList();

        return View(new CreateClassInformationByCustomer());
    }


    // POST <ClassInformationController/CreateClassInformation>
    //[Authorize]
    [ValidateAntiForgeryToken]
    [HttpPost]
    [Route("CreateClassInformation")]
    public async Task<IActionResult> CreateClassInformation(
        CreateClassInformationByCustomer createUpdateClassInformationDto)
    {
        var command = _mapper.Map<CreateUpdateClassInformationCommand>(createUpdateClassInformationDto);

        var result = await _mediator.Send(command);

        return View("SuccessPage");
    }


    // PUT api/<ClassInformationController>/5
    [Authorize]
    [HttpPost]
    [Route("UpdateClassInformation")]
    public async Task<IActionResult> UpdateClassInformation(
        CreateUpdateClassInformationDto createUpdateClassInformationDto)
    {
        var command = _mapper.Map<CreateUpdateClassInformationCommand>(createUpdateClassInformationDto);

        var result = await _mediator.Send(command);

        return Ok(result);
    }

 

    [HttpPost]
    [Route("RequestGettingClass")]
    public async Task<IActionResult> RequestGettingClass(RequestGettingClassRequest requestGettingClassRequest)
    {
        var email = HttpContext.Request.Cookies["email"];

        if (User.Identity is null || !User.Identity.IsAuthenticated || email is null)
        {
            string? returnUrl = Url.Action("RequestGettingClass");
            HttpContext.Session.SetString("Value", requestGettingClassRequest.ClassId.ToString());
            return RedirectToAction("Index", "Authentication",new{returnUrl = returnUrl});
        }
      

        requestGettingClassRequest.Email = email;
        var command = _mapper.Map<RequestGettingClassCommand>(requestGettingClassRequest);
        var result = await _mediator.Send(command);
        if (result.IsFailed)
        {
            ViewBag.RequestedMessage = result.Reasons.FirstOrDefault()?.Message ?? "";
            return View("FailPage");
        }
        return View("SuccessRequestPage");


    }
}