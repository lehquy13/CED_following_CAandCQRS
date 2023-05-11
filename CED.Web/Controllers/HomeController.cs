using System.Diagnostics;
using Castle.Core.Internal;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.Users.Queries;
using CED.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using CED.Web.Models;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace CED.Web.Controllers;

record secrect(string name, List<int> data);

[Authorize]
[Route("[controller]")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IMapper _mapper;
    private readonly ISender _sender;

    public HomeController(ILogger<HomeController> logger, IMapper mapper, ISender sender)
    {
        _logger = logger;
        _mapper = mapper;
        _sender = sender;
    }

    [Route("")]
    public async Task<IActionResult> Index()
    {
        var classInfors = await _sender.Send(new GetAllClassInformationsQuery());
        var tutorDtos = await _sender.Send(new GetUsersQuery<TutorDto>());
        var studentDtos = await _sender.Send(new GetUsersQuery<StudentDto>());


        List<int> dates = new List<int>();

        var startday = DateTime.Today.Subtract(TimeSpan.FromDays(6));

        for (int i = 0; i < 7; i++)
        {
            dates.Add(startday.Day);
            startday = startday.AddDays(1);
        }

        startday = DateTime.Today.Subtract(TimeSpan.FromDays(6));


        var classesInWeek = dates.GroupJoin(
                classInfors
                    .Where(x => x.CreationTime >= startday)
                    .GroupBy(x => x.CreationTime.Day),
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.FirstOrDefault()?.Count() ?? 0
                })
            .Select(x => x.classInfo)
            .ToList();
        var studentsInWeek = dates.GroupJoin(
                studentDtos
                    .Where(x => x.CreationTime >= startday)
                    .GroupBy(x => x.CreationTime.Day),
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.FirstOrDefault()?.Count() ?? 0
                })
            .Select(x => x.classInfo)
            .ToList();

        var tutorsInWeek = dates.GroupJoin(
                studentDtos
                    .Where(x => x.CreationTime >= startday)
                    .GroupBy(x => x.CreationTime.Day),
                d => d,
                c => c.Key,
                (d, c) => new
                {
                    dates = d,
                    classInfo = c.FirstOrDefault()?.Count() ?? 0
                })
            .Select(x => x.classInfo)
            .ToList();

        secrect[] chartWeekData = new[]
        {
            new secrect(
                "Classes",
                classesInWeek
            ),
            new secrect(
                "Tutors",
                tutorsInWeek
            ),
            new secrect(
                "Students",
                studentsInWeek
            ),
        };
        var datesWeekData = new JsonResult(new
        {
            type = "string",
            categories = dates.ToArray()
        });

        var check = JsonConvert.SerializeObject(chartWeekData);
        return View(
            new DashBoardViewModel
            {
                StudentDtos = studentDtos,
                ClassInformationDtos = classInfors,
                TutorDtos = tutorDtos,
                ChartWeekData = check,
                DatesWeekData = JsonConvert.SerializeObject(datesWeekData.Value)
            }
        );
    }

    [Route("Privacy")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Route("Error")]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}