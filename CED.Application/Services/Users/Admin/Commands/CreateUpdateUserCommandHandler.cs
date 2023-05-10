using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Interfaces.Logger;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Users.Admin.Commands;

public class CreateUpdateUserCommandHandler : CreateUpdateCommandHandler<CreateUpdateUserCommand>
{

    private readonly IUserRepository _userRepository;
    private readonly IAppLogger<CreateUpdateUserCommandHandler> _logger;

    public CreateUpdateUserCommandHandler(IUserRepository userRepository,IAppLogger<CreateUpdateUserCommandHandler> logger, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public override async Task<bool> Handle(CreateUpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(command.UserDto.Email);
            //Check if the subject existed
            if (user is not null)
            {
                user.UpdateUserInformation(_mapper.Map<User>(command.UserDto));
                _logger.LogDebug("ready for updating!");
                _userRepository.Update(user);

                return true;
            }
            _logger.LogDebug("ready for creating!");

            user = _mapper.Map<User>(command.UserDto);

            await _userRepository.Insert(user);

            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when user is adding or updating." + ex.Message);
        }
    }
}

