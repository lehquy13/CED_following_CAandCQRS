using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Interfaces.Services;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Student.Commands;

public class LearnerInfoChangingCommandHandler : CreateUpdateCommandHandler<LearnerInfoChangingCommand>
{
    private readonly IUserRepository _userRepository; 
    private readonly ICloudinaryFile _cloudinaryFile;

    public LearnerInfoChangingCommandHandler(IUserRepository userRepository,
        ILogger<LearnerInfoChangingCommandHandler> logger,
        ICloudinaryFile cloudinaryFile,
        IMapper mapper) : base(logger,mapper)
    {
        _userRepository = userRepository;
        _cloudinaryFile = cloudinaryFile;
    }
    public override async Task<bool> Handle(LearnerInfoChangingCommand command,  CancellationToken cancellationToken) 
    {
        var user = await _userRepository.GetUserByEmail(command.LearnerDto.Email);
        //Check if the subject existed
        if (user is not null)
        {
            if (!string.IsNullOrWhiteSpace(command.FilePath))
            {
                command.LearnerDto.Image = _cloudinaryFile.UploadImage(command.FilePath);
            }
            user.UpdateUserInformation(_mapper.Map<User>(command.LearnerDto));
            _logger.LogDebug("ready for updating!");
            _userRepository.Update(user);
                

            return true;
        }
        _logger.LogDebug("ready for creating!");

        user = _mapper.Map<User>(command.LearnerDto);

        await _userRepository.Insert(user);

        return true;
    }
}

