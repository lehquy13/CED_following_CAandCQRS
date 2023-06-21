using Abp.Domain.Entities.Auditing;

namespace CED.Domain.Review;

public class TutorReview : AuditedEntity<Guid>
{
    public Guid TutorId { get; set; }
    public Guid LearnerId { get; set; }
    public short Rate { get; set; } = 5;
    public string Description { get; set; } = "";
}