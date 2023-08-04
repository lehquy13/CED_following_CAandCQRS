using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts;
using CED.Contracts.TutorReview;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Review;
using CED.Domain.Users;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.TutorReviews.Queries;

public class GetAllReviewByIdQueryHandler : GetAllQueryHandler<GetAllReviewByIdQuery, TutorReviewDto>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IRepository<TutorReview> _tutorReviewRepository;

    public GetAllReviewByIdQueryHandler(ITutorRepository tutorRepository,IRepository<TutorReview> tutorReviewRepository, IMapper mapper, IClassInformationRepository classInformationRepository):base(mapper)
    {
        _tutorRepository = tutorRepository;
        _tutorReviewRepository = tutorReviewRepository;
        _classInformationRepository = classInformationRepository;
    }

    public override async Task<Result<PaginatedList<TutorReviewDto>>> Handle(GetAllReviewByIdQuery query, CancellationToken cancellationToken)
    {
        await Task.CompletedTask;
        try
        {
            var resultsFromDb = await _tutorRepository.GetReviewsOfTutor(query.ObjectId);
            var resultDtos = _mapper.Map<List<TutorReviewDto>>(resultsFromDb);
            
            //testing mapping paginatedlist 
            return PaginatedList<TutorReviewDto>.CreateAsync(resultDtos,query.PageIndex,query.PageSize,resultDtos.Count);
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message);
        }
    }
}

