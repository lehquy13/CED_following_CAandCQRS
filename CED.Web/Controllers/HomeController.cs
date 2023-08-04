using System.Diagnostics;
using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Queries.GetAllClassInformationsQuery;
using CED.Application.Services.DashBoard.Queries;
using CED.Application.Services.Users.Queries.CustomerQueries;
using CED.Contracts;
using CED.Contracts.Charts;
using CED.Contracts.ClassInformations.Dtos;
using CED.Contracts.Users;
using CED.Domain.Shared.ClassInformationConsts;
using Microsoft.AspNetCore.Mvc;
using CED.Web.Models;
using CED.Web.Utilities;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json;
using CED.Contracts.Notifications;

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
        var tutorDtos = await _sender.Send(new GetAllTutorInformationsAdvancedQuery());
        var studentDtos = await _sender.Send(new GetObjectQuery<PaginatedList<LearnerDto>>());
        _logger.LogDebug("Got classDtos, tutorDtos, studentDtos!");

        var date = GetByTime(DateTime.Now, ByTime.Month);
        var thisMonthClassInformationDtoCount = classDtos.Where(x => x.CreationTime >= date).Count();
        var thisMonthTutorDtoCount = tutorDtos.Where(x => x.CreationTime >= date).Count();
        var thisMonthStudentDtoCount = studentDtos.Where(x => x.CreationTime >= date).Count();

        date = GetByTime(date, ByTime.Month);
        var lastMonthClassInformationDtoCount = classDtos.Where(x => x.CreationTime >= date).Count();
        var lastMonthresultTutorDtoCount = tutorDtos.Where(x => x.CreationTime >= date).Count();
        var lastMonthStudentDtoCount = studentDtos.Where(x => x.CreationTime >= date).Count();


        _logger.LogDebug("On getting lineChartData, donutChartData...");
        LineChartData lineChartData = await _sender.Send(new GetLineChartDataQuery(""));
        DonutChartData donutChartData = await _sender.Send(new GetDonutChartDataQuery(""));
        var datesWeekData = new ChartDataType(
            "string",
            lineChartData.dates
        );
        _logger.LogDebug("Got lineChartData, donutChartData!");

        var chartWeekData = JsonConvert.SerializeObject(lineChartData.LineDatas);
        var pieWeekDataNames = JsonConvert.SerializeObject(donutChartData.names);
        var pieWeekDataValues = JsonConvert.SerializeObject(donutChartData.values);
        var dateWeekData = JsonConvert.SerializeObject(datesWeekData);

        var areaListData = await AreaChartDataCalculate(ByTime.Week);

        _logger.LogDebug("On getting recent activities...");
        List<NotificationDto> notificationDtos = await _sender.Send(new GetNotificationQuery());
       
        _logger.LogInformation("Got recent activities! Serializing and return.");
        return View(
            new DashBoardViewModel
            {
                StudentTotalValueModel = new TotalValueModel<LearnerDto>()
                {
                    Models = studentDtos,
                    IsIncrease = thisMonthStudentDtoCount > lastMonthStudentDtoCount,
                    IncreasePercentage = Math.Abs(thisMonthStudentDtoCount - lastMonthStudentDtoCount) * 1.0 /
                                         lastMonthStudentDtoCount * 100,
                    Time = ByTime.Month
                },
                ClassTotalValueModel = new TotalValueModel<ClassInformationDto>()
                {
                    Models = classDtos,
                    IsIncrease = thisMonthClassInformationDtoCount > lastMonthClassInformationDtoCount,
                    IncreasePercentage =
                        Math.Abs(thisMonthClassInformationDtoCount - lastMonthClassInformationDtoCount) * 1.0 /
                        lastMonthClassInformationDtoCount * 100,
                    Time = ByTime.Month
                },
                TutorTotalValueModel = new TotalValueModel<TutorDto>()
                {
                    Models = tutorDtos,
                    IsIncrease = thisMonthTutorDtoCount > lastMonthresultTutorDtoCount,
                    IncreasePercentage = Math.Abs(thisMonthTutorDtoCount - lastMonthresultTutorDtoCount) * 1.0 /
                                         lastMonthresultTutorDtoCount * 100,
                    Time = ByTime.Month
                },

                ChartWeekData = chartWeekData,
                // Chart
                PieWeekData1 = pieWeekDataValues,
                PieWeekData2 = pieWeekDataNames,
                DatesWeekData = dateWeekData,
                //Incomes Chart
                AreaChartViewModel = new AreaChartViewModel()
                {
                    dates = areaListData.ElementAt(0),
                    totalRevenueSeries = areaListData.ElementAt(1),
                    refundedSeries = areaListData.ElementAt(2),
                    incomingSeries = areaListData.ElementAt(3),
                    ByTime =  ByTime.Week
                },
                NotificationDtos = notificationDtos
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

    private async Task<List<string>> AreaChartDataCalculate(string? byTime)
    {
        _logger.LogDebug("FitlerPieChart's running! On getting DonutChartData...");
        AreaChartData areaChartData = await _sender.Send(new GetAreaChartDataQuery(byTime ?? ""));
        _logger.LogDebug("Got donutChartData! Serializing and return.");
        
        var check1 = JsonConvert.SerializeObject(areaChartData.dates);
        var check2 = JsonConvert.SerializeObject(areaChartData.totalRevuenues.data);
        var check3 = JsonConvert.SerializeObject(areaChartData.cenceleds.data);
        var check4 = JsonConvert.SerializeObject(areaChartData.incoming.data);

        return new List<string>
        {
            check1, check2, check3, check4
        };

    }
    [HttpGet]
    [Route("FilterAreaChart/{byTime?}")]
    public async Task<IActionResult> FilterAreaChart(string? byTime)
    {
        var listData = await AreaChartDataCalculate(byTime);
        return Helper.RenderRazorViewToString(this, "_AreaChart",
            new AreaChartViewModel()
            {
                dates = listData.ElementAt(1),
                totalRevenueSeries = listData.ElementAt(1),
                refundedSeries = listData.ElementAt(3),
                incomingSeries = listData.ElementAt(4),
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
    [Route("FilterTotalTutors/{byTime?}")]
    public async Task<IActionResult> FilterTotalTutors(string? byTime)
    {
        _logger.LogDebug("Index's running! On getting tutorDtos...");
        var tutorDtos = await _sender.Send(new GetAllTutorInformationsAdvancedQuery());
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
        var studentDtos = await _sender.Send(new GetObjectQuery<PaginatedList<LearnerDto>>());
        _logger.LogDebug("Got studentDtos!");

        var date = GetByTime(DateTime.Now, byTime);
        var result1 = studentDtos.Where(x => x.CreationTime >= date).ToList();
        var date2 = GetByTime(date, byTime);
        var result2 = studentDtos.Where(x => x.CreationTime >= date2 && x.CreationTime <= date).ToList();


        return Helper.RenderRazorViewToString(this, "_TotalStudents", new TotalValueModel<LearnerDto>()
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
            case ByTime.Week:
                date = date.Subtract(TimeSpan.FromDays(6));
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