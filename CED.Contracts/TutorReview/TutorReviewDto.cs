using Abp.Application.Services.Dto;

namespace CED.Contracts.TutorReview;

public class TutorReviewDto : EntityDto<Guid>
{
    public Guid TutorId { get; set; }
    public Guid LearnerId { get; set; }
    public short Rate { get; set; } = 5;
    public string Description { get; set; } = "";
}