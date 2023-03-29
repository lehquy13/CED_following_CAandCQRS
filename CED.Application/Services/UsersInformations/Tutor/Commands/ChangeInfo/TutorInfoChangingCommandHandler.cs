using CED.Domain.Users;
using CED.Domain.Shared.ClassInformationConsts;
using MapsterMapper;
using CED.Application.Common.Services.CommandHandlers;

namespace CED.Application.Services.UsersInformations.Tutor.Commands.ChangeInfo;

public class TutorInfoChangingCommandHandler : CreateUpdateCommandHandler<TutorInfoChangingCommand>
{
    private readonly IUserRepository _userRepository;
    public TutorInfoChangingCommandHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<bool> Handle(TutorInfoChangingCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetUserByEmail(command.TutorDto.Email);
        if (user is null)
        {
            throw new Exception("User with an email doesn't exist");
        }
        if (user.Role != UserRole.Tutor) return false;

        user.UpdateTutorInformation(_mapper.Map<User>(command.TutorDto));

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

