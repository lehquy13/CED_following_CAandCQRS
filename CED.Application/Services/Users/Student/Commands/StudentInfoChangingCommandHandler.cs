using CED.Domain.Users;
using CED.Domain.Shared.ClassInformationConsts;
using MapsterMapper;
using CED.Application.Services.Abstractions.CommandHandlers;

namespace CED.Application.Services.Users.TutorRegister.Commands;

public class StudentInfoChangingCommandHandler : CreateUpdateCommandHandler<StudentInfoChangingCommand>
{
    private readonly IUserRepository _userRepository;
    public StudentInfoChangingCommandHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }
    public override async Task<bool> Handle(StudentInfoChangingCommand command, CancellationToken cancellationToken)
    {
        //Check if the user existed
        var user = await _userRepository.GetUserByEmail(command.StudentDto.Email);
        if (user is null)
        {
            throw new Exception("User with an email doesn't exist");
        }
        if (user.Role != UserRole.Student) return false;

        user.UpdateUserInformation(_mapper.Map<User>(command.StudentDto));

        var afterUpdatedUser = _userRepository.Update(user);

        if (afterUpdatedUser is null) { return false; }

        return true;
    }
}

