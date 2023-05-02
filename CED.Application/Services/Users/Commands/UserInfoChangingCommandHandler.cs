using CED.Domain.Users;
using CED.Domain.Shared.ClassInformationConsts;
using MapsterMapper;
using CED.Application.Services.Abstractions.CommandHandlers;
using Abp.Domain.Entities;

namespace CED.Application.Services.Users.Commands;

public class UserInfoChangingCommandHandler
    : CreateUpdateCommandHandler<UserInfoChangingCommand>
{
    private readonly IUserRepository _userRepository;
    public UserInfoChangingCommandHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<bool> Handle(UserInfoChangingCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetUserByEmail(command.UserDto.Email);
        if (user is null)
        {
            throw new Exception("User with an email doesn't exist");
        }

       

        user.UpdateUserInformation(_mapper.Map<User>(command.UserDto));

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

