using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Domain.Common.Models;
using CED.Domain.Interfaces.Services;
using CED.Domain.Shared.NotificationConsts;
using CED.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Admin.Commands;

public class CreateUpdateUserCommandHandler : CreateUpdateCommandHandler<CreateUpdateUserCommand>
{

    private readonly IUserRepository _userRepository;
    private readonly ICloudinaryFile _cloudinaryFile;
    private readonly IPublisher _publisher;

    public CreateUpdateUserCommandHandler(IUserRepository userRepository,
        ILogger<CreateUpdateUserCommandHandler> logger, IPublisher publisher,
        ICloudinaryFile cloudinaryFile,
        IMapper mapper) : base(logger,mapper)
    {
        _userRepository = userRepository;
        _cloudinaryFile = cloudinaryFile;
        _publisher = publisher;
    }

    public override async Task<bool> Handle(CreateUpdateUserCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(command.UserDto.Email);
            //Check if the subject existed
            if (user is not null)
            {
                if (!string.IsNullOrWhiteSpace(command.FilePath))
                {
                    command.UserDto.Image = _cloudinaryFile.UploadImage(command.FilePath);
                }
                user.UpdateUserInformation(_mapper.Map<User>(command.UserDto));
                _logger.LogDebug("ready for updating!");
                _userRepository.Update(user);
                

                return true;
            }
            _logger.LogDebug("ready for creating!");
            user = _mapper.Map<User>(command.UserDto);

           var entity =  await _userRepository.Insert(user);
            var message = "New learner: " + entity.FirstName + " " + entity.LastName + " at " + entity.CreationTime.ToLongDateString();
            await _publisher.Publish(new NewObjectCreatedEvent(entity.Id, message, NotificationEnum.Learner), cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when user is adding or updating." + ex.Message);
        }
    }
}

