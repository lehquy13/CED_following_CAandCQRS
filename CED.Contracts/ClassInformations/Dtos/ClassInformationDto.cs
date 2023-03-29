using Abp.Application.Services.Dto;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.ClassInformations;

public class ClassInformationDto : FullAuditedEntityDto<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.Waiting;
    public LearningMode LearningMode { get; set; } = LearningMode.Offline;

    public float Fee { get; set; } = 0;
    public float ChargeFee { get; set; } = 0;

    //Tutor related information
    public Gender GenderRequirement { get; set; } = Gender.None;
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Optional;

    //Student related information
    public Gender StudentGender { get; set; } = Gender.None;
    public int NumberOfStudent { get; set; } = 1;

    // Time related information
    public int MinutePerSession { get; set; } = 90;
    public int SessionPerWeek { get; set; } = 2;

    //public List<Guid> AvailableDateIds { get; set; }
    //public List<AvailableDate> AvailableDates { get; set; }

    // Address related information
    public string Address { get; set; } = string.Empty;

    //Subject related information
    public Guid SubjectId { get; set; }
    public string SubjectName { get; set; } = string.Empty;

    //Confirmed data related

    public Guid? TutorId { get; set; }
    public Guid? StudentId { get; set; }
}

