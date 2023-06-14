using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Application.Services.Users.Student.Queries;
using CED.Application.Services.Users.Tutor.Registers;
using CED.Contracts;
using CED.Contracts.Interfaces.Services;
using CED.Contracts.Subjects;
using CED.Contracts.Users;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Web.CustomerSide.Utilities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CED.Web.CustomerSide.Controllers;

[Route("[controller]")]
public class TutorInformationController : Controller
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    private readonly IAddressService _addressService;
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly int _pageSize = 12;

    public TutorInformationController(ISender mediator,IWebHostEnvironment webHostEnvironment, IMapper mapper, IAddressService addressService)
    {
        _mediator = mediator;
        _mapper = mapper;
        _addressService = addressService;
        _webHostEnvironment = webHostEnvironment;
    }

    private async Task PackStaticListToView()
    {
        ViewData["Subjects"] = await _mediator.Send(new GetObjectQuery<PaginatedList<SubjectDto>>());
    }

    // Query
    // GET: api/<TutorInformationController>
    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index(int pageIndex = 1, string subjectName = "", int birthYear = 0,
        string gender = "", string academicLevel = "", string address = "")
    {
        await PackStaticListToView();
        var genderSearch = Gender.None;
        var academicLevelSearch = AcademicLevel.Optional;
        if (!string.IsNullOrWhiteSpace(subjectName))
        {
            ViewBag.SubjectSearch = subjectName;
        }

        if (birthYear != 0)
        {
            ViewBag.BirthYearSearch = birthYear;
        }

        if (!string.IsNullOrWhiteSpace(address))
        {
            ViewBag.AddressSearch = address;
        }

        if (!string.IsNullOrWhiteSpace(gender) && gender != "Gender")
        {
            ViewBag.GenderSearch = gender;
            genderSearch = (Gender)Enum.Parse(typeof(Gender), gender, true);
        }

        if (!string.IsNullOrWhiteSpace(academicLevel) && academicLevel != "Academic Level")
        {
            ViewBag.AcademicLevelSearch = academicLevel;
            academicLevelSearch = (AcademicLevel)Enum.Parse(typeof(AcademicLevel), academicLevel, true);
        }

        var query = new GetAllTutorInformationsAdvancedQuery()
        {
            PageSize = _pageSize,
            PageIndex = pageIndex,
            SubjectName = subjectName,
            BirthYear = birthYear,
            Gender = genderSearch,
            Academic = academicLevelSearch,
            Address = address
        };
        var tutorDtos = await _mediator.Send(query);


        return View(tutorDtos);
    }

    // GET api/<TutorInformationController>/5
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Detail(Guid id)
    {
        var query = new GetObjectQuery<TutorDto>()
        {
            Guid = id
        };
        var tutorDto = await _mediator.Send(query);

        return View(tutorDto);
    }
    
    [Authorize]
    [HttpGet]
    [Route("TutorRegistration")]
    public async Task<IActionResult> TutorRegistration()
    {
        var email = HttpContext.Session.GetString("email");
        if (email is not null)
        {
            var query = new GetLearnerByMailQuery()
            {
                Email = email
            };
            var result = await _mediator.Send(query);
            if (result != null)
                return View(_mapper.Map<TutorDto>(result));
        }

        return View(new TutorDto());
    }

    // POST <TutorInformationController>
    [Authorize]
    [HttpPost]
    [Route("TutorRegistration")]
    public async Task<IActionResult> TutorRegistration(TutorDto tutorDto, List<string>? SubjectId, List<string>? filePaths)
    {
        if (filePaths != null)
        {
            for (var i =0; i <filePaths.Count;i++)
            {
                filePaths[i] = _webHostEnvironment.WebRootPath + filePaths.ElementAt(i);
            
            }
        }

            var command = new TutorRegistrationCommand(tutorDto, SubjectId, filePaths);

            var result = await _mediator.Send(command);

            Helper.ClearTempFile(_webHostEnvironment.WebRootPath);
            if (result)
            {
                return View("SuccessPage"); //implement
            }

        return View("FailPage"); //implement
    }
}