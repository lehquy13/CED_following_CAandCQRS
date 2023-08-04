using CED.Contracts.Models;
using CED.Contracts.Subjects;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.ClassInformations.Dtos;

public class ClassInformationDto : FullAuditedAggregateRootDto<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Status Status { get; set; } = Status.OnVerifying;
    public LearningMode LearningMode { get; set; } = LearningMode.Offline;

    public float Fee { get; set; } = 0;
    public float ChargeFee { get; set; } = 0;

    //Tutor related information

    public Gender GenderRequirement { get; set; } = Gender.None;
    public AcademicLevel AcademicLevelRequirement { get; set; } = AcademicLevel.Optional;

    //Student related information
    //public string StudentName { get; set; } = String.Empty;
    //public string StudentPhoneNumber { get; set; } = String.Empty;
    public string LearnerName { get; set; } = string.Empty;

    public Gender LearnerGender { get; set; } = Gender.Male;
    public int NumberOfLearner { get; set; } = 1;
    
    public string ContactNumber { get; set; } = string.Empty;
    public Guid? LearnerId { get; set; }


    // Time related information
    public int MinutePerSession { get; set; } = 90;
    public int SessionPerWeek { get; set; } = 2;

    // Address related information
    public string Address { get; set; } = string.Empty;

    //Subject related information
    public Guid SubjectId { get; set; }
    public SubjectDto Subject { get; set; }
    public Guid? TutorId { get; set; }

    //Request of class
    public List<RequestGettingClassDto> RequestGettingClasses { get; set; } = new List<RequestGettingClassDto>();
}

