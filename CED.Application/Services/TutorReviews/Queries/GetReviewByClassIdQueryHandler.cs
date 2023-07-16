using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.TutorReview;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Review;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.TutorReviews.Queries;

public class GetReviewByClassIdQueryHandler : GetByIdQueryHandler<GetObjectQuery<TutorReviewDto>, TutorReviewDto>
{
  
    private readonly IRepository<TutorReview> _tutorReviewRepository;

    public GetReviewByClassIdQueryHandler(IRepository<TutorReview> tutorReviewRepository, IMapper mapper):base(mapper)
    {
        _tutorReviewRepository = tutorReviewRepository;
    }
    public override async Task<TutorReviewDto?> Handle(GetObjectQuery<TutorReviewDto> query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {

            var review = _tutorReviewRepository.GetAll().FirstOrDefault(x => x.ClassInformationId == query.Guid); 
            //testing mapping paginatedlist
            if (review is null)
            {
                return null;
            }
            return _mapper.Map<TutorReviewDto>(review);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

