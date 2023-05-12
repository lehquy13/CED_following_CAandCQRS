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
        _logger.LogDebug("Index's running! On getting classDtos, tutorDtos, studentDtos...");
        var classDtos = await _sender.Send(new GetAllClassInformationsQuery());
        var tutorDtos = await _sender.Send(new GetUsersQuery<TutorDto>());
        var studentDtos = await _sender.Send(new GetUsersQuery<StudentDto>());
        _logger.LogDebug("Got classDtos, tutorDtos, studentDtos!");

        _logger.LogDebug("On getting lineChartData, donutChartData...");
        LineChartData lineChartData = await _sender.Send(new GetLineChartDataQuery(classDtos,studentDtos,tutorDtos,""));
        DonutChartData donutChartData = await _sender.Send(new GetDonutChartDataQuery(classDtos,""));
        var datesWeekData = new ChartDataType(
        
            "string",
            lineChartData.dates
        );
        _logger.LogDebug("Got lineChartData, donutChartData! Serializing and return.");

        var check = JsonConvert.SerializeObject(lineChartData.LineDatas);
        var check2 = JsonConvert.SerializeObject(donutChartData.names);
        var check1 = JsonConvert.SerializeObject(donutChartData.values);
        var check3 = JsonConvert.SerializeObject(datesWeekData);
        return View(
            new DashBoardViewModel
            {
                StudentDtos = studentDtos,
                ClassInformationDtos = classDtos,
                TutorDtos = tutorDtos,
                ChartWeekData = check,
                PieWeekData1 = check1,
                PieWeekData2 = check2,
                DatesWeekData =check3
            }
        );
    }
    
    [HttpGet]
    [Route("FitlerLineChart/{byTime?}")]
    public async Task<IActionResult> FitlerLineChart(string? byTime)
    {
        _logger.LogDebug("Index's running! On getting classDtos, tutorDtos, studentDtos...");
        var classDtos = await _sender.Send(new GetAllClassInformationsQuery());
        var tutorDtos = await _sender.Send(new GetUsersQuery<TutorDto>());
        var studentDtos = await _sender.Send(new GetUsersQuery<StudentDto>());
        _logger.LogDebug("Got classDtos, tutorDtos, studentDtos!");

        _logger.LogDebug("On getting lineChartData...");
        LineChartData lineChartData = await _sender.Send(new GetLineChartDataQuery(classDtos,studentDtos,tutorDtos,byTime??""));
        var datesWeekData = new ChartDataType(
        
             "string",
            lineChartData.dates
        );
        var check = JsonConvert.SerializeObject(lineChartData.LineDatas);
        var check1 = JsonConvert.SerializeObject(datesWeekData);

        return Json(new
        {
            ChartWeekData = check,
            DatesWeekData = check1

        });
    }
    [HttpGet]
    [Route("FitlerPieChart/{byTime?}")]
    public async Task<IActionResult> FitlerPieChart(string? byTime)
    {
        _logger.LogDebug("Index's running! On getting classDtos, tutorDtos, studentDtos...");
        var classDtos = await _sender.Send(new GetAllClassInformationsQuery());
        
        DonutChartData donutChartData = await _sender.Send(new GetDonutChartDataQuery(classDtos,byTime??""));

        _logger.LogDebug("Got lineChartData, donutChartData! Serializing and return.");

        var check2 = JsonConvert.SerializeObject(donutChartData.names);
        var check1 = JsonConvert.SerializeObject(donutChartData.values);

        return Json(new
        {
            pieWeekData1 = check1,
            pieWeekData2 = check2,

        });
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