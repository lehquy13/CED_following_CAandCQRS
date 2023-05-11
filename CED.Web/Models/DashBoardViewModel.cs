using Microsoft.AspNetCore.Mvc;

namespace CED.Web.Models;

public class DashBoardViewModel
{
    public List<CED.Contracts.Users.TutorDto> TutorDtos { get; set; } = new();
    public List<CED.Contracts.Users.StudentDto> StudentDtos { get; set; } = new();
    public List<CED.Contracts.ClassInformations.ClassInformationDto> ClassInformationDtos { get; set; } = new();
    public object? ChartWeekData { get; set; } 
    public object? DatesWeekData { get; set; } 
    
    public List<CED.Contracts.Subjects.SubjectDto> SubjectDtos { get; set; } = new();
    
}