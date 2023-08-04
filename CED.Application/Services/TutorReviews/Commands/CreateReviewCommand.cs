using CED.Contracts.TutorReview;
using FluentResults;
using MediatR;

namespace CED.Application.Services.TutorReviews.Commands;

public class CreateReviewCommand
    : IRequest<Result<bool>>
{
    public TutorReviewDto ReviewDto { get; set; } = null!;
    public string LearnerEmail { get; set; } = null!;
    public string TutorEmail { get; set; } = null!;
    public Guid ClassInformationId { get; set; }

}

