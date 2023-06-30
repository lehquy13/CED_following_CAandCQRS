using CED.Domain.Common.Models;

namespace CED.Domain.Review;

public class TutorReview : AuditedEntity<Guid>
{
    public Guid ClassInformationId { get; set; }
    public short Rate { get; set; } = 5;
    public string Description { get; set; } = "";
}