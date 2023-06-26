using Abp.Application.Services.Dto;

namespace CED.Contracts.TutorReview;

public class TutorReviewDto : AuditedEntityDto<Guid>
{
    public Guid TutorId { get; set; }
    public Guid LearnerId { get; set; }
    public string LearnerName { get; set; }
    public Guid ClassInformationId { get; set; }

    public short Rate { get; set; } = 5;
    public string Description { get; set; } = "";
}
public class TutorReviewRequestDto : AuditedEntityDto<Guid>
{
    public string LearnerEmail { get; set; } = string.Empty;
    public string TutorEmail { get; set; }= string.Empty;
    public string ClassId { get; set; }= string.Empty;

    public short Rate { get; set; } = 5;
    public string Description { get; set; } = "";
}