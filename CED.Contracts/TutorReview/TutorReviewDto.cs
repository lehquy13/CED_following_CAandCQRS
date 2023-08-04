using CED.Contracts.Common.Models;
using CED.Contracts.Models;

namespace CED.Contracts.TutorReview;

public class TutorReviewDto : FullAuditedAggregateRootDto<Guid>
{
    public Guid TutorId { get; set; }
    public Guid LearnerId { get; set; }
    public string LearnerName { get; set; }=string.Empty;
    public Guid ClassInformationId { get; set; }

    public short Rate { get; set; } = 5;
    public string Description { get; set; } =string.Empty;
}
public class TutorReviewRequestDto : FullAuditedAggregateRootDto<Guid>
{
    public string LearnerEmail { get; set; } = string.Empty;
    public string TutorEmail { get; set; }= string.Empty;
    public string ClassId { get; set; }= string.Empty;

    public short Rate { get; set; } = 5;
    public string Description { get; set; } = "";
}