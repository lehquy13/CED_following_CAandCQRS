using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Repository;
using CED.Domain.Users;
using FluentResults;
using LazyCache;
using MediatR;


namespace CED.Application.Services.Users.Admin.Commands;

public class RemoveTutorVerificationCommandHandler : DeleteCommandHandler<RemoveTutorVerificationCommand>
{
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationRepository;

    public RemoveTutorVerificationCommandHandler(IRepository<TutorVerificationInfo> tutorVerificationRepository,
        IUnitOfWork unitOfWork, IPublisher publisher, IAppCache cache) : base(unitOfWork,cache, publisher)
    {
        _tutorVerificationRepository = tutorVerificationRepository;
    }

    public override async Task<Result<bool>> Handle(RemoveTutorVerificationCommand command, CancellationToken cancellationToken)
    {
        if (command.Guid == Guid.Empty)
            return false;
        if (await _tutorVerificationRepository.DeleteById(command.Guid))
        {
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
                return Result.Fail("Error while deleting tutor verification");
            return true;
        }
        return Result.Fail("Tutor verification not found");
    }
}
