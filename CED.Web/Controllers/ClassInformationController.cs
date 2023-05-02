using Microsoft.AspNetCore.Mvc;
using MapsterMapper;
using MediatR;
using CED.Contracts.ClassInformations;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Commands;
using CED.Domain.Shared;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;
using CED.Web.Utilities;

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
    private async Task PackStaticListToView(HttpContext HttpContext)
    {
        ViewData["Roles"] = CEDEnumProvider.Roles;
        ViewData["Genders"] = CEDEnumProvider.Genders;
        ViewData["AcademicLevels"] = CEDEnumProvider.AcademicLevels;
        ViewData["LearningModes"] = CEDEnumProvider.LearningModes;
        ViewData["Statuses"] = CEDEnumProvider.Status;

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


    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var query = new GetAllClassInformationsQuery();
        var classInformations = await _mediator.Send(query);

        return View(classInformations);
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(Guid Id)
    {
        await PackStaticListToView(this.HttpContext);


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
        ViewBag.Updated = true;
        return await Edit(Id);
    }

    [HttpGet("Create")]
    public async Task<IActionResult> Create()
    {
        await PackStaticListToView(this.HttpContext);

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

        return Json(new
        {
            html = Helper.RenderRazorViewToString(this, "Delete", result)

        });
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


}

