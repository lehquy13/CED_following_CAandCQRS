﻿using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.Subjects.Commands;
using CED.Contracts;
using CED.Contracts.Subjects;
using CED.Web.CustomerSide.Utilities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CED.Web.CustomerSide.Controllers;

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
    
    [HttpGet("Subjects")]
    public async Task<IActionResult> Subjects(string? tutorGuid)
    {
        var query = new GetObjectQuery<PaginatedList<SubjectDto>>();
        
        if (tutorGuid != null)
        {
            query.Guid = new Guid(tutorGuid);
        }
        var subjectDtos = await _mediator.Send(query);
        return Helper.RenderRazorViewToString(this, "_Subjects", subjectDtos);
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

