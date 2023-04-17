using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using CED.Web.Models;
using CED.Contracts.Subjects;
using MapsterMapper;
using MediatR;
using CED.Application.Services.Subjects.Queries;

namespace CED.Web.Controllers;

[Route("[controller]")]
//[Authorize]
public class SubjectController : Controller
{
    private readonly ILogger<SubjectController> _logger;
    //dependencies 
    private readonly ISender _mediator;
    private readonly IMapper _mapper;

    public List<SubjectDto> subjectDtos { get; set; } = new List<SubjectDto>();

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
        var query = new GetAllSubjectsQuery();
        subjectDtos = await _mediator.Send(query);

        return View(subjectDtos);
    }


    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

