using Abp.Application.Services.Dto;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.ClassInformations.Dtos;

public class ClassInformationDto : FullAuditedEntityDto<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.OnConfirming;
    public LearningMode LearningMode { get; set; } = LearningMode.Offline;

    public float Fee { get; set; } = 0;
    public float ChargeFee { get; set; } = 0;

    //Tutor related information
    public Gender GenderRequirement { get; set; } = Gender.None;
    public AcademicLevel AcademicLevel { get; set; } = AcademicLevel.Optional;

    //Student related information
    public Gender StudentGender { get; set; } = Gender.None;
    public int NumberOfStudent { get; set; } = 1;
    public string ContactNumber { get; set; } = string.Empty;
    public Guid? StudentId { get; set; }


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

    public Guid? TutorDtoId { get; set; }
    public string TutorName { get; set; } = string.Empty;
    public string TutorPhoneNumber { get; set; } = string.Empty;
    public string TutorEmail { get; set; } = string.Empty;
}

