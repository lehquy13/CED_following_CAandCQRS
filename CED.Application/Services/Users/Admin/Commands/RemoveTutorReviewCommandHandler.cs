using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Repository;
using CED.Domain.Review;
using CED.Domain.Users;


namespace CED.Application.Services.Users.Admin.Commands;

public class RemoveTutorReviewCommandHandler : DeleteCommandHandler<RemoveTutorReviewCommand>
{
    private readonly IRepository<TutorReview> _tutorReviewnRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveTutorReviewCommandHandler(IRepository<TutorReview> tutorReviewnRepository, IUnitOfWork unitOfWork)
    {
        _tutorReviewnRepository = tutorReviewnRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<bool> Handle(RemoveTutorReviewCommand command, CancellationToken cancellationToken)
    {
        if (command.Guid == Guid.Empty)
            return false;
        if (await _tutorReviewnRepository.DeleteById(command.Guid))
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;

    }
}
