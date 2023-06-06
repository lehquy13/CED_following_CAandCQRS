using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.Users.Tutor.Commands.Registers;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.ClassInformations.Tutor.Commands.Registers;

public class TutorRegisterCommandHandler : CreateUpdateCommandHandler<TutorRegisterCommand>
{
    private readonly ITutorRepository _userRepository;
    public TutorRegisterCommandHandler(ITutorRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<bool> Handle(TutorRegisterCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetUserByEmail(command.TutorDto.Email);
        if (user is null)
        {
            throw new Exception("User with an email doesn't exist");
        }
        if (user.Role == UserRole.Tutor) return false;

        user.UpdateTutorInformation(_mapper.Map<Domain.Users.Tutor>(command.TutorDto));

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

