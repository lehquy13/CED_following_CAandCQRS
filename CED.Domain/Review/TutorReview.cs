using Abp.Domain.Entities;

namespace CED.Domain.Review;

public class TutorReview : Entity<Guid>
{
    public Guid TutorId { get; set; }
    public Guid LearnerId { get; set; }
    public short Rate { get; set; } = 5;
    public string Description { get; set; } = "";
}