using System.Diagnostics;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.DashBoard.Queries;
using CED.Application.Services.Users.Queries;
using CED.Contracts.Charts;
using CED.Contracts.ClassInformations;
using CED.Contracts.Users;
using CED.Domain.Shared.ClassInformationConsts;
using Microsoft.AspNetCore.Mvc;
using CED.Web.Models;
using CED.Web.Utilities;
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

        var date = GetByTime(DateTime.Now, ByTime.Month);
        var resultClassInformationDtos1 = classDtos.Where(x => x.CreationTime >= date).ToList();
        var resultTutorDtos1 = tutorDtos.Where(x => x.CreationTime >= date).ToList();
        var resultStudentDtos1 = studentDtos.Where(x => x.CreationTime >= date).ToList();

        date = GetByTime(date, ByTime.Month);
        var resultClassInformationDtos2 = classDtos.Where(x => x.CreationTime >= date).ToList();
        var resultTutorDtos2 = tutorDtos.Where(x => x.CreationTime >= date).ToList();
        var resultStudentDtos2 = studentDtos.Where(x => x.CreationTime >= date).ToList();


        _logger.LogDebug("On getting lineChartData, donutChartData...");
        LineChartData lineChartData = await _sender.Send(new GetLineChartDataQuery(""));
        DonutChartData donutChartData = await _sender.Send(new GetDonutChartDataQuery(""));
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
                StudentTotalValueModel = new TotalValueModel<StudentDto>()
                {
                    Models = studentDtos,
                    IsIncrease = resultStudentDtos1.Count > resultStudentDtos2.Count,
                    IncreasePercentage = Math.Abs(resultStudentDtos1.Count - resultStudentDtos2.Count) * 1.0 /
                                         resultStudentDtos2.Count * 100,
                    Time = ByTime.Month
                },
                ClassTotalValueModel = new TotalValueModel<ClassInformationDto>()
                {
                    Models = classDtos,
                    IsIncrease = resultClassInformationDtos1.Count > resultClassInformationDtos2.Count,
                    IncreasePercentage =
                        Math.Abs(resultClassInformationDtos1.Count - resultClassInformationDtos2.Count) * 1.0 /
                        resultClassInformationDtos2.Count * 100,
                    Time = ByTime.Month
                },
                TutorTotalValueModel = new TotalValueModel<TutorDto>()
                {
                    Models = tutorDtos,
                    IsIncrease = resultTutorDtos1.Count > resultTutorDtos2.Count,
                    IncreasePercentage = Math.Abs(resultTutorDtos1.Count - resultTutorDtos2.Count) * 1.0 /
                                         resultTutorDtos2.Count * 100,
                    Time = ByTime.Month
                },
                ChartWeekData = check,
                PieWeekData1 = check1,
                PieWeekData2 = check2,
                DatesWeekData = check3
            }
        );
    }

    [HttpGet]
    [Route("FitlerLineChart/{byTime?}")]
    public async Task<IActionResult> FitlerLineChart(string? byTime)
    {
        _logger.LogDebug("On getting lineChartData...");
        LineChartData lineChartData = await _sender.Send(new GetLineChartDataQuery(byTime ?? ""));
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
        _logger.LogDebug("FitlerPieChart's running! On getting DonutChartData...");
        DonutChartData donutChartData = await _sender.Send(new GetDonutChartDataQuery(byTime ?? ""));
        _logger.LogDebug("Got donutChartData! Serializing and return.");

        var check2 = JsonConvert.SerializeObject(donutChartData.names);
        var check1 = JsonConvert.SerializeObject(donutChartData.values);

        return Helper.RenderRazorViewToString(this, "_PieChart",
            new PieChartViewModel()
            {
                labels = check2,
                series = check1,
                ByTime = byTime ?? ByTime.Today
            });
    }
    [HttpGet]
    [Route("FilterAreaChart/{byTime?}")]
    public async Task<IActionResult> FilterAreaChart(string? byTime)
    {
        _logger.LogDebug("FitlerPieChart's running! On getting DonutChartData...");
        AreaChartData areaChartData = await _sender.Send(new GetAreaChartDataQuery(byTime ?? ""));
        _logger.LogDebug("Got donutChartData! Serializing and return.");

        var check2 = JsonConvert.SerializeObject(areaChartData.totalRevuenues.data);
        var check3 = JsonConvert.SerializeObject(areaChartData.cenceleds.data);
        var check4 = JsonConvert.SerializeObject(areaChartData.incoming.data);
        var check1 = JsonConvert.SerializeObject(areaChartData.dates);

        return Helper.RenderRazorViewToString(this, "_AreaChart",
            new AreaChartViewModel()
            {
                dates = check1,
                totalRevenueSeries = check2,
                refundedSeries = check3,
                incomingSeries = check4,
                ByTime = byTime ?? ByTime.Today
            });
    }

    [HttpGet]
    [Route("FitlerTotalClasses/{byTime?}")]
    public async Task<IActionResult> FitlerTotalClasses(string? byTime)
    {
        _logger.LogDebug("Index's running! On getting classDtos...");
        var classDtos = await _sender.Send(new GetAllClassInformationsQuery());
        _logger.LogDebug("Got classDtos!");

        var date = GetByTime(DateTime.Now, byTime);
        var result1 = classDtos.Where(x => x.CreationTime >= date).ToList();
        var date2 = GetByTime(date, byTime);
        var result2 = classDtos.Where(x => x.CreationTime >= date2 && x.CreationTime <= date).ToList();

        return Helper.RenderRazorViewToString(this, "_TotalClasses", new TotalValueModel<ClassInformationDto>()
        {
            Models = result1,
            IsIncrease = result1.Count > result2.Count,
            IncreasePercentage = (Math.Abs(result1.Count - result2.Count) * 1.0 / result2.Count) * 100,
            Time = byTime ?? "Today"
        });
    }

    [HttpGet]
    [Route("FitlerTotalTutors/{byTime?}")]
    public async Task<IActionResult> FitlerTotalTutors(string? byTime)
    {
        _logger.LogDebug("Index's running! On getting tutorDtos...");
        var tutorDtos = await _sender.Send(new GetUsersQuery<TutorDto>());
        _logger.LogDebug("Got tutorDtos!");
        var date = GetByTime(DateTime.Now, byTime);
        var result1 = tutorDtos.Where(x => x.CreationTime >= date).ToList();
        var date2 = GetByTime(date, byTime);

        var result2 = tutorDtos.Where(x => x.CreationTime >= date2 && x.CreationTime <= date).ToList();

        return Helper.RenderRazorViewToString(this, "_TotalTutors", new TotalValueModel<TutorDto>()
        {
            Models = result1,
            IsIncrease = result1.Count > result2.Count,
            IncreasePercentage = Math.Abs(result1.Count - result2.Count) * 1.0 / result2.Count * 100,
            Time = byTime ?? "Today"
        });
    }

    [HttpGet]
    [Route("FitlerTotalStudents/{byTime?}")]
    public async Task<IActionResult> FitlerTotalStudents(string? byTime)
    {
        _logger.LogDebug("Index's running! On getting studentDtos...");
        var studentDtos = await _sender.Send(new GetUsersQuery<StudentDto>());
        _logger.LogDebug("Got studentDtos!");

        var date = GetByTime(DateTime.Now, byTime);
        var result1 = studentDtos.Where(x => x.CreationTime >= date).ToList();
        var date2 = GetByTime(date, byTime);
        var result2 = studentDtos.Where(x => x.CreationTime >= date2 && x.CreationTime <= date).ToList();


        return Helper.RenderRazorViewToString(this, "_TotalStudents", new TotalValueModel<StudentDto>()
        {
            Models = result1,
            IsIncrease = result1.Count > result2.Count,
            IncreasePercentage = Math.Abs(result1.Count - result2.Count) * 1.0 / result2.Count * 100,
            Time = byTime ?? "Today"
        });
    }

    private DateTime GetByTime(DateTime date, string? byTime)
    {
        switch (byTime)
        {
            case ByTime.Month:
                date = date.Subtract(TimeSpan.FromDays(29));
                break;
            case ByTime.Year:
                date = date.Subtract(TimeSpan.FromDays(364));
                break;
            default:
                if (date.Hour == 0)
                {
                    return date.Subtract(TimeSpan.FromDays(1));
                }
                else
                {
                    date = date.Subtract(date.TimeOfDay);
                }

                break;
        }

        return date;
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