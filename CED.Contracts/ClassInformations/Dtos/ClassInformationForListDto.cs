using CED.Contracts.Models;
using CED.Domain.Shared.ClassInformationConsts;

namespace CED.Contracts.ClassInformations.Dtos;

public sealed class ClassInformationForListDto : FullAuditedAggregateRootDto<Guid>
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Status { get; set; } =  CED.Domain.Shared.ClassInformationConsts.Status.OnVerifying.ToString();
    public string LearningMode { get; set; } = CED.Domain.Shared.ClassInformationConsts.LearningMode.Offline.ToString();

    public float Fee { get; set; } = 0;
    public float ChargeFee { get; set; } = 0;

    //Tutor related information
    public string GenderRequirement { get; set; } =  Gender.None.ToString();
    public string AcademicLevel { get; set; } = Domain.Shared.ClassInformationConsts.AcademicLevel.Optional.ToString();

    //Student related information
    public string LearnerGender { get; set; } =  Gender.None.ToString();
    public int NumberOfLearner { get; set; } = 1;
    public string ContactNumber { get; set; } = string.Empty;
    public Guid? LearnerId { get; set; }

    // Time related information
    public int MinutePerSession { get; set; } = 90;
    public int SessionPerWeek { get; set; } = 2;

    // Address related information
    public string Address { get; set; } = string.Empty;

    //Subject related information
    public string SubjectName { get; set; } = string.Empty;
  
}