using System.Diagnostics;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.DashBoard.Queries;
using CED.Application.Services.Users.Queries;
using CED.Contracts.Charts;
using CED.Contracts.Users;
using Microsoft.AspNetCore.Mvc;
using CED.Web.Models;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;

namespace CED.Web.Controllers;
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
        var classDtos = await _sender.Send(new GetAllClassInformationsQuery());
        var tutorDtos = await _sender.Send(new GetUsersQuery<TutorDto>());
        var studentDtos = await _sender.Send(new GetUsersQuery<StudentDto>());
        
        LineChartData lineChartData = await _sender.Send(new GetLineChartDataQuery(classDtos,studentDtos,tutorDtos,""));
        DonutChartData donutChartData = await _sender.Send(new GetDonutChartDataQuery(classDtos,""));
        var datesWeekData = new JsonResult(new
        {
            type = "string",
            categories = lineChartData.dates
        });

        var check = JsonConvert.SerializeObject(lineChartData.LineDatas);
        var check1 = JsonConvert.SerializeObject(donutChartData.DonutDatas);
        return View(
            new DashBoardViewModel
            {
                StudentDtos = studentDtos,
                ClassInformationDtos = classDtos,
                TutorDtos = tutorDtos,
                ChartWeekData = check,
                PieWeekData = check1,
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