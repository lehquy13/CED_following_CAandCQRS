using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Student.Commands;

public class StudentInfoChangingCommandHandler : CreateUpdateCommandHandler<StudentInfoChangingCommand>
{
    private readonly IUserRepository _userRepository;
    public StudentInfoChangingCommandHandler(IUserRepository userRepository,ILogger<StudentInfoChangingCommandHandler> logger, IMapper mapper) : base(logger,mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<bool> Handle(StudentInfoChangingCommand command,  CancellationToken cancellationToken) 
    {
        //Check if the user existed
        var user = await _userRepository.GetUserByEmail(command.StudentDto.Email);
        if (user is null)
        {
            throw new Exception("User with an email doesn't exist");
        }
        if (user.Role != UserRole.Learner) return false;

        user.UpdateUserInformation(_mapper.Map<User>(command.StudentDto));

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

