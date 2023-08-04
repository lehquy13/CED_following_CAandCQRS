using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Domain.Repository;
using CED.Domain.Shared.NotificationConsts;
using CED.Domain.Users;
using FluentResults;
using LazyCache;
using MediatR;

namespace CED.Application.Services.Users.Admin.Commands;

public class DeleteUserCommandHandler
    : DeleteCommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork, IAppCache cache,
        IPublisher publisher) : base(unitOfWork, cache,publisher)
    {
        _userRepository = userRepository;
    }
    public override async Task<Result<bool>> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetById(command.UserId);
        if (user is null)
        {
            return Result.Fail(new NonExistUserError());
        }
        
        //user.DeleterUserId
        user.DeletionTime = DateTime.Now;
        user.IsDeleted = true;

        if (await _unitOfWork.SaveChangesAsync() <= 0)
        {
            return Result.Fail("Fail to delete user");
        }
        var message = "Delete tutor: " + user.GetFullNAme();
        await _publisher.Publish(new NewObjectCreatedEvent(user.Id, message, NotificationEnum.Tutor), cancellationToken);
        
        return true;
    }
}

