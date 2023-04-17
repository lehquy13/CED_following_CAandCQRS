using CED.Application.Common.Services.CommandHandlers;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;

namespace CED.Application.Services.Subjects.Commands;

public class CreateUserCommandHandler : CreateUpdateCommandHandler<CreateUserCommand>
{

    private readonly IUserRepository _userRepository;

    public CreateUserCommandHandler(IUserRepository userRepository, IMapper mapper) : base(mapper)
    {
        _userRepository = userRepository;
    }

    public override async Task<bool> Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetById(command.UserDto.Id);
            //Check if the subject existed
            if (user is not null)
            {
                user.LastModificationTime = DateTime.Now;
                user.Description = command.UserDto.Description;

                _userRepository.Update(user);

                return true;
            }

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

