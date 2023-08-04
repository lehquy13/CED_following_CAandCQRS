using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.TutorReview;
using CED.Domain.ClassInformations;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.TutorReviews.Queries;

public class GetReviewByClassIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<TutorReviewDto>, TutorReviewDto>
{
  
    private readonly IClassInformationRepository _classInformationRepository;

    public GetReviewByClassIdQueryHandler(IClassInformationRepository classInformationRepository, IMapper mapper):base(mapper)
    {
        _classInformationRepository = classInformationRepository;
    }
    public override async Task<Result<TutorReviewDto>> Handle(GetObjectQuery<TutorReviewDto> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            if (query.ObjectId == Guid.Empty)
            {
                return Result.Fail("Review not found");
            }
            var review = await _classInformationRepository.GetReviewByClassId(query.ObjectId);
            if (review is null)
            {
                return Result.Fail("Review not found");
            }
            return _mapper.Map<TutorReviewDto>(review);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

