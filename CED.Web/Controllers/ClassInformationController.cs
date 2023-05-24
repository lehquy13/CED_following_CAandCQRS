using CED.Application.Services;
using CED.Application.Services.Abstractions.QueryHandlers;
using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;
using CED.Contracts.ClassInformations;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Commands;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using CED.Web.Utilities;
using CED.Application.Services.Users.Queries;
using CED.Contracts.Users;
using CED.Domain.Shared;

namespace CED.Web.Controllers;

[Route("[controller]")]
[Authorize]
public class ClassInformationController : Controller
{
    private readonly ILogger<ClassInformationController> _logger;
    //dependencies 
    private readonly ISender _mediator;
    private readonly IMapper _mapper;


    public ClassInformationController(ILogger<ClassInformationController> logger, ISender sender, IMapper mapper)
    {
        _logger = logger;
        _mediator = sender;
        _mapper = mapper;


    }
    private async Task PackStaticListToView()
    {
        ViewData["Roles"] = EnumProvider.Roles;
        ViewData["Genders"] = EnumProvider.Genders;
        ViewData["AcademicLevels"] = EnumProvider.AcademicLevels;
        ViewData["LearningModes"] = EnumProvider.LearningModes;
        ViewData["Statuses"] = EnumProvider.Status;

        var value = HttpContext.Session.GetString("SubjectList");
        if (value is null)
        {
            var subjectLookupDtos = await _mediator.Send(new GetAllSubjectsLookUpQuery(HttpContext));
            ViewData["Subjects"] = subjectLookupDtos;
            HttpContext.Session.SetString("SubjectList", JsonConvert.SerializeObject(subjectLookupDtos));
        }
        else
        {
            ViewData["Subjects"] = JsonConvert.DeserializeObject<List<SubjectLookupDto>>(value);
        }
    }
    private async Task PackStudentAndTuTorList()
    {
        var tutorDtos = await _mediator.Send(new GetUsersQuery<TutorDto>());
        var studentDtos = await _mediator.Send(new GetUsersQuery<StudentDto>());
        ViewData["TutorDtos"] = tutorDtos;
        ViewData["StudentDtos"] = studentDtos;

    }



    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var query = new GetObjectQuery<List<ClassInformationDto>>();
        var classInformations = await _mediator.Send(query);

        return View(classInformations);
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(Guid Id)
    {
        await PackStaticListToView();

        await PackStudentAndTuTorList();

        var query = new GetClassInformationQuery()
        {
            Id = Id
        };
        var result = await _mediator.Send(query);
        ViewBag.Action = "Edit";

        return View(result);
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid Id, ClassInformationDto classDto)
    {
        if (Id != classDto.Id)
        {
            return NotFound();
        }
        if (!ModelState.IsValid)
        {
            return View(classDto);
        }
        var query = new CreateUpdateClassInformationCommand()
        {
            ClassInformationDto = classDto
        };

        var result = await _mediator.Send(query);

        if (result is false)
        {
            return View(classDto);
        }
        await PackStaticListToView();
        await PackStudentAndTuTorList();
        
        return Helper.RenderRazorViewToString(this,"Edit",classDto);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        await PackStaticListToView();
        //await PackStudentAndTuTorList();
        
        return View(new ClassInformationDto());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ClassInformationDto classDto)
    {
        classDto.LastModificationTime = DateTime.UtcNow;
        var query = new CreateUpdateClassInformationCommand() { ClassInformationDto = classDto };
        var result = await _mediator.Send(query);

        return RedirectToAction("Index");
    }

    [HttpGet("Delete")]
    public async Task<IActionResult> Delete(Guid? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var query = new GetClassInformationQuery() { Id = (Guid)id };
        var result = await _mediator.Send(query);

        if (result == null)
        {
            return NotFound();
        }

        return

            Helper.RenderRazorViewToString(this, "Delete", result);


    }

    [HttpPost("DeleteConfirmed")]
    public async Task<IActionResult> DeleteConfirmed(Guid? id)
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new DeleteClassInformationCommand((Guid)id);
        var result = await _mediator.Send(query);

        if (result is true)
        {
            return RedirectToAction("Index");

        }
        return RedirectToAction("Error", "Home");
    }

    [HttpGet("Detail")]
    public async Task<IActionResult> Detail(Guid? id)
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new GetClassInformationQuery() { Id = (Guid)id };

        var result = await _mediator.Send(query);

        if (result is not null)
        {
            return View(result);

        }
        return RedirectToAction("Error", "Home");
    }
    
    [HttpGet]
    [Route("PickTutor")]
    public async Task<IActionResult> PickTutor()
    {
        var query = new GetUsersQuery<TutorDto>();
        var userDtos = await _mediator.Send(query);
        return Helper.RenderRazorViewToString(this, "PickTutor", userDtos);


    }
    [HttpGet("ViewTutor")]
    public async Task<IActionResult> ViewTutor(Guid? id) 
    {
        if (id == null || id.Equals(Guid.Empty))
        {
            return NotFound();
        }

        var query = new GetUserByIdQuery<TutorDto>() { Id= (Guid)id };
        var result = await _mediator.Send(query);

        if (result is not null)
        {
            return Helper.RenderRazorViewToString(this, "ViewTutor", result);

        }
        return RedirectToAction("Error", "Home");
    }
    [HttpPost("Choose")]
    public  IActionResult Choose(Guid? tutorId) 
    {
        if (tutorId == null || tutorId.Equals(Guid.Empty))
        {
            return NotFound();
        }

        return Json(new {tutorId = tutorId});
    }

}

