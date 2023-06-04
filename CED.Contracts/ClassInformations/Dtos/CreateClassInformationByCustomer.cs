using System.ComponentModel.DataAnnotations;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.ClassInformations.Dtos;

public class CreateClassInformationByCustomer
{
    [Required]
    [StringLength(128)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    public float Fee { get; set; } = 0;
    
    // Address related information
    [Required]
    public string Address { get; set; } = string.Empty;
    
    // Time related information
    public int MinutePerSession { get; set; } = 90;
    public int SessionPerWeek { get; set; } = 2;
    
    //Student related information
    [Required]
    public Gender StudentGender { get; set; } = Gender.Male;
    [Required]
    public int NumberOfStudent { get; set; } = 1;
    [Required]
    public string ContactNumber { get; set; } = string.Empty;

    public Guid? StudentId { get; set; }
 
    //Tutor related information
    public Gender GenderRequirement { get; set; } = Gender.None;
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Optional;
    
    //Subject related information
    [Required]
    public Guid SubjectId { get; set; }
    
    public string SubjectName { get; set; } = string.Empty;
}