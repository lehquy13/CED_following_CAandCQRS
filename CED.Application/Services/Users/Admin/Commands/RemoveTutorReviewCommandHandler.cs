using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Repository;
using CED.Domain.Review;
using FluentResults;
using LazyCache;
using MediatR;


namespace CED.Application.Services.Users.Admin.Commands;

public class RemoveTutorReviewCommandHandler : DeleteCommandHandler<RemoveTutorReviewCommand>
{
    private readonly IRepository<TutorReview> _tutorReviewnRepository;

    public RemoveTutorReviewCommandHandler(IRepository<TutorReview> tutorReviewnRepository, IUnitOfWork unitOfWork,
        IPublisher publisher, IAppCache cache) : base(unitOfWork,cache,publisher)
    {
        _tutorReviewnRepository = tutorReviewnRepository;
    }

    public override async Task<Result<bool>> Handle(RemoveTutorReviewCommand command, CancellationToken cancellationToken)
    {
        if (command.Guid == Guid.Empty)
            return false;
        if (await _tutorReviewnRepository.DeleteById(command.Guid))
        {
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return Result.Fail("Error while deleting tutor review");
            }
            return true;
        }
        return Result.Fail("Tutor review not found");

    }
}
