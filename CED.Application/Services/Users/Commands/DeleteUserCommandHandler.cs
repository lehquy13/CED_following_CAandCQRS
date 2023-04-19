using CED.Domain.Users;
using CED.Application.Common.Services.CommandHandlers;

namespace CED.Application.Services.Users.Commands;

public class DeleteUserCommandHandler
    : DeleteCommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository _userRepository;
    public DeleteUserCommandHandler(IUserRepository userRepository) : base()
    {
        _userRepository = userRepository;
    }
    public override async Task<bool> Handle(DeleteUserCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetById(command.guid);
        if (user is null)
        {
            throw new Exception("User with an id doesn't exist");
        }
        
        
        //user.DeleterUserId
        user.DeletionTime = DateTime.Now;
        user.IsDeleted = true;

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

