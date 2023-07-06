using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Repository;
using CED.Domain.Users;


namespace CED.Application.Services.Users.Admin.Commands;

public class RemoveTutorVerificationCommandHandler : DeleteCommandHandler<RemoveTutorVerificationCommand>
{
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveTutorVerificationCommandHandler(IRepository<TutorVerificationInfo> tutorVerificationRepository, IUnitOfWork unitOfWork)
    {
        _tutorVerificationRepository = tutorVerificationRepository;
        _unitOfWork = unitOfWork;
    }

    public override async Task<bool> Handle(RemoveTutorVerificationCommand command, CancellationToken cancellationToken)
    {
        if (command.Guid == Guid.Empty)
            return false;
        if (await _tutorVerificationRepository.DeleteById(command.Guid))
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return true;
        }
        return false;

    }
}
