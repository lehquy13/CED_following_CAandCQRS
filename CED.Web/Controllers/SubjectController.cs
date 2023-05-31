﻿using CED.Application.Services.Abstractions.QueryHandlers;
using Microsoft.AspNetCore.Mvc;
using CED.Contracts.Subjects;
using MapsterMapper;
using MediatR;
using CED.Application.Services.Subjects.Queries;
using CED.Application.Services.Subjects.Commands;
using CED.Contracts;
using Microsoft.AspNetCore.Authorization;
using CED.Web.Utilities;

namespace CED.Web.Controllers;

[Route("[controller]")]
[Authorize]
public class SubjectController : Controller
{
    private readonly ILogger<SubjectController> _logger;
    //dependencies 
    private readonly ISender _mediator;
    private readonly IMapper _mapper;



    public SubjectController(ILogger<SubjectController> logger, ISender sender, IMapper mapper)
    {
        _logger = logger;
        _mediator = sender;
        _mapper = mapper;
    }

    [HttpGet]
    [Route("")]
    public async Task<IActionResult> Index()
    {
        var query = new GetObjectQuery<PaginatedList<SubjectDto>>();
        var subjectDtos = await _mediator.Send(query);

        return View(subjectDtos);
    }
    [HttpGet]
    [Route("{id}")]
    public async Task<IActionResult> Index(int i)
    {
        var query = new GetObjectQuery<PaginatedList<SubjectDto>>()
        {
            PageIndex = 2,
            PageSize = 10,
            
        };
        var subjectDtos = await _mediator.Send(query);

        return View(subjectDtos);
    }

    [HttpGet("Edit")]
    public async Task<IActionResult> Edit(Guid Id)
    {
       
        var query = new GetObjectQuery<SubjectDto>()
        {
            Guid = Id
        };
        var result = await _mediator.Send(query);

        return View(result);
    }

    [HttpPost("Edit")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid Id, SubjectDto subjectDto)
    {
        if (Id != subjectDto.Id)
        {
            return NotFound();
        }
        if (ModelState.IsValid)
        {
            try
            {
                var query = new CreateUpdateSubjectCommand()
                {
                    SubjectDto = subjectDto
                };
                var result = await _mediator.Send(query);
                ViewBag.Updated = true;
                if (result)
                {
                    return Helper.RenderRazorViewToString(this,"Edit",subjectDto);
                }


            }
            catch (Exception ex)
            {
                //Log the error (uncomment ex variable name and write a log.)
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists, " + ex.Message +
                    "see your system administrator.");
            }
        }
        return View(subjectDto);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View(new SubjectDto());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(SubjectDto subjectDto) // cant use userdto
    {
        subjectDto.LastModificationTime = DateTime.UtcNow;
        var query = new CreateUpdateSubjectCommand() { SubjectDto = subjectDto };
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

        var query = new GetObjectQuery<SubjectDto>() { Guid = (Guid)id };
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

        var query = new DeleteSubjectCommand((Guid)id);
        var result = await _mediator.Send(query);

        if (result is true)
        {
            return Json(new { res = "deleted" }) ;

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

        var query = new GetObjectQuery<SubjectDto>() { Guid = (Guid)id };

        var result = await _mediator.Send(query);

        if (result is not null)
        {
            return View(result);

        }
        return RedirectToAction("Error", "Home");
    }


}

