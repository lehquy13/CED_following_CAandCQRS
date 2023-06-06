using CED.Contracts.ClassInformations.Dtos;

namespace CED.Web.Models;

public class DashBoardViewModel
{
    public TotalValueModel<CED.Contracts.Users.TutorDto> TutorTotalValueModel{ get; set; } = new();
    public TotalValueModel<CED.Contracts.Users.LearnerDto> StudentTotalValueModel{ get; set; } = new();

    public TotalValueModel<ClassInformationDto> ClassTotalValueModel{ get; set; } = new();

    public object? ChartWeekData { get; set; }
    public object? PieWeekData1 { get; set; }
    public object? PieWeekData2 { get; set; }
    public object? DatesWeekData { get; set; } 
    public AreaChartViewModel AreaChartViewModel { get; set; } 
    
    public List<CED.Contracts.Subjects.SubjectDto> SubjectDtos { get; set; } = new();
    
}

public class PieChartViewModel
{
    public object? series { get; set; } 
    public object? labels { get; set; }

    public string ByTime = Domain.Shared.ClassInformationConsts.ByTime.Today;
}
public class AreaChartViewModel
{
    public string totalRevenue = "Total Revenues";
    public string refunded = "Refunded";
    public string incoming = "Incomings";
    public string? totalRevenueSeries { get; set; } 
    public string? refundedSeries { get; set; } 
    public string? incomingSeries { get; set; } 
    public string? dates { get; set; }

    public string ByTime = Domain.Shared.ClassInformationConsts.ByTime.Today;
}