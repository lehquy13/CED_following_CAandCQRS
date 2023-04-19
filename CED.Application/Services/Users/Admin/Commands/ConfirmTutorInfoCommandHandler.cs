using CED.Domain.Users;
using CED.Domain.Shared.ClassInformationConsts;
using MapsterMapper;
using CED.Application.Common.Services.CommandHandlers;
using CED.Application.Services.Users.Admin.Commands.ChangeInfo;

namespace CED.Application.Services.Users.Tutor.Commands.ChangeInfo;

public class ConfirmTutorInfoCommandHandler : CreateUpdateCommandHandler<ConfirmTutorInfoCommand>
{
    private readonly IUserRepository _userRepository;
    public ConfirmTutorInfoCommandHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<bool> Handle(ConfirmTutorInfoCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetUserByEmail(command.TutorDto.Email);
        if (user is null)
        {
            throw new Exception("Tutor with an email doesn't exist");
        }
        if (user.Role != UserRole.Tutor) return false;

        command.TutorDto.isVerified = true;
        user.UpdateTutorInformation(_mapper.Map<User>(command.TutorDto));

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

