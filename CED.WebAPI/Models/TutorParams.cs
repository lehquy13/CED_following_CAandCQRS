namespace CED.WebAPI.Models;

public class TutorParams
{
    public int PageIndex{ get; set; } = 1;
    public string? SubjectName { get; set; } = "";
    public string? Gender { get; set; }= "";
    public int? BirthYear { get; set; }
    public string? AcademicLevel { get; set; }= "";
    public string? Address { get; set; }= "";
}