using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.TutorReview;

namespace CED.Application.Services.TutorReviews.Queries;

public class GetAllReviewByIdQuery : GetObjectQuery<PaginatedList<TutorReviewDto>>
{
    
}