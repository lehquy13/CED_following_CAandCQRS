namespace CED.Web.Models;

public class DashBoardViewModel
{
    public TotalValueModel<CED.Contracts.Users.TutorDto> TutorTotalValueModel{ get; set; } = new();
    public TotalValueModel<CED.Contracts.Users.StudentDto> StudentTotalValueModel{ get; set; } = new();

    public TotalValueModel<CED.Contracts.ClassInformations.ClassInformationDto> ClassTotalValueModel{ get; set; } = new();

    
    
    public object? ChartWeekData { get; set; }
    public object? PieWeekData1 { get; set; }
    public object? PieWeekData2 { get; set; }
    public object? DatesWeekData { get; set; } 
    
    public List<CED.Contracts.Subjects.SubjectDto> SubjectDtos { get; set; } = new();
    
}