using CED.Contracts.Models;
using CED.Contracts.Subjects;
using CED.Contracts.TutorReview;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.ClassInformations.Dtos;

public class ClassInformationForDetailDto : FullAuditedAggregateRootDto<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } = Domain.Shared.ClassInformationConsts.Status.OnVerifying.ToString();
    public string LearningMode { get; set; } = Domain.Shared.ClassInformationConsts.LearningMode.Offline.ToString();

    public float Fee { get; set; } = 0;
    public float ChargeFee { get; set; } = 0;

    //Tutor related information
    public string GenderRequirement { get; set; } = Domain.Shared.ClassInformationConsts.Gender.None.ToString();
    public string AcademicLevel { get; set; } = Domain.Shared.ClassInformationConsts.AcademicLevel.Optional.ToString();

    //Student related information
    public string LearnerName { get; set; } = "";
    public string LearnerGender { get; set; } = Domain.Shared.ClassInformationConsts.Gender.None.ToString();
    public int NumberOfLearner { get; set; } = 1;
    public string ContactNumber { get; set; } = string.Empty;
    public Guid? LearnerId { get; set; }


    // Time related information
    public int MinutePerSession { get; set; } = 90;
    public int SessionPerWeek { get; set; } = 2;

    //public List<Guid> AvailableDateIds { get; set; }
    //public List<AvailableDate> AvailableDates { get; set; }

    // Address related information
    public string Address { get; set; } = string.Empty;

    //Subject related information
    public Guid SubjectId { get; set; }
    public SubjectDto Subject { get; set; } = null!;


    //Confirmed data related
    public Guid? TutorId { get; set; }
    public string TutorName { get; set; } = string.Empty;
    public string TutorPhoneNumber { get; set; } = string.Empty;
    public string TutorEmail { get; set; } = string.Empty;
    
    //List of Request

    public List<RequestGettingClassMinimalDto> RequestGettingClassDtos = new List<RequestGettingClassMinimalDto>();
    public string TutorReviewDto { get; set; }= "";
    public string TutorReviewDtoId { get; set; }= "";
}

