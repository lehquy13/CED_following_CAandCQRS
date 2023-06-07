using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.ClassInformations.Tutor.Commands.ChangeInfo;

public class TutorInfoChangingCommandHandler : CreateUpdateCommandHandler<TutorInfoChangingCommand>
{
    private readonly ITutorRepository _userRepository;
    public TutorInfoChangingCommandHandler(ITutorRepository userRepository,ILogger<TutorInfoChangingCommandHandler> logger, IMapper mapper) : base(logger,mapper)
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

        user.UpdateTutorInformation(_mapper.Map<Domain.Users.Tutor>(command.TutorDto));

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

