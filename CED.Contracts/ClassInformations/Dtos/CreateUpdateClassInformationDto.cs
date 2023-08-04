using System.ComponentModel.DataAnnotations;
using CED.Contracts.Common.Models;
using CED.Contracts.Models;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.ClassInformations.Dtos;


public class CreateUpdateClassInformationDto : FullAuditedAggregateRootDto<Guid>
{
    [Required]
    [StringLength(128)]
    public string Title { get; set; } = string.Empty;

    [Required]
    public string Description { get; set; } = string.Empty;

    [Required]
    public Status Status { get; set; } = Status.OnVerifying;

    [Required]
    public LearningMode LearningMode { get; set; } = LearningMode.Offline;

    [Required]
    public float Fee { get; set; } = 0;
    [Required]
    public float ChargeFee { get; set; } = 0;

    //Tutor related information
    [Required]
    public Gender GenderRequirement { get; set; } = Gender.None;
    [Required]
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Optional;

    //Student related information
    [Required]
    public Gender StudentGender { get; set; } = Gender.Male;
    [Required]
    public int NumberOfStudent { get; set; } = 1;
    
    public string ContactNumber { get; set; } = string.Empty;

    public Guid? StudentId { get; set; }


    // Time related information
    [Required]
    public int MinutePerSession { get; set; } = 90;
    [Required]
    public int SessionPerWeek { get; set; } = 2;

    //[Required]
    //public List<Guid> AvailableDates { get; set; }

    // Address related information
    [Required]
    public string Address { get; set; } = string.Empty;

    //Subject related information
    [Required]

    public Guid SubjectId { get; set; }
    
    public Guid? TutorId { get; set; }
}

