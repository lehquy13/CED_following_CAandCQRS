using CED.Domain.Users;
using CED.Domain.Shared.ClassInformationConsts;
using MediatR;
using MapsterMapper;

namespace CED.Application.Services.Authentication.Commands.Register;


public class TutorRegisterCommandHandler
    : IRequestHandler<TutorRegisterCommand, bool>
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    public TutorRegisterCommandHandler(IUserRepository userRepository, IMapper mapper)
    {
        _userRepository = userRepository;
        _mapper = mapper;
    }
    public async Task<bool> Handle(TutorRegisterCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetUserByEmail(command.TutorDto.Email);
        if (user is null)
        {
            //  return new AuthenticationResult(false, "User has already existed");
            throw new Exception("User with an email hasn't existed");
        }
        if (user.Role == UserRole.Tutor) return false;

        user.TutorRegistration(_mapper.Map<User>(command.TutorDto));

        var afterUpdatedUser = _userRepository.Update(user);
        
        if(afterUpdatedUser is null) { return false; }

        return true;
    }

   
}

