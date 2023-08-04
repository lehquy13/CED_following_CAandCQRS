using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Domain.Common.Models;
using CED.Domain.Interfaces.Services;
using CED.Domain.Repository;
using CED.Domain.Shared.NotificationConsts;
using CED.Domain.Users;
using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Admin.Commands;

public class CreateUpdateUserCommandHandler : CreateUpdateCommandHandler<CreateUpdateUserCommand>
{
    private readonly IUserRepository _userRepository;
    private readonly ICloudinaryFile _cloudinaryFile;

    public CreateUpdateUserCommandHandler(IUserRepository userRepository,
        ILogger<CreateUpdateUserCommandHandler> logger, IPublisher publisher,
        ICloudinaryFile cloudinaryFile,
        IMapper mapper, IAppCache cache, IUnitOfWork unitOfWork) : base(logger, mapper, unitOfWork, cache, publisher)
    {
        _userRepository = userRepository;
        _cloudinaryFile = cloudinaryFile;
    }

    public override async Task<Result<bool>> Handle(CreateUpdateUserCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            var user = await _userRepository.GetUserByEmail(command.UserDto.Email);
            //Check if the user existed
            if (user is not null)
            {
                //Update user
                if (!string.IsNullOrWhiteSpace(command.FilePath))
                {
                    command.UserDto.Image = _cloudinaryFile.UploadImage(command.FilePath);
                }

                user.UpdateUserInformation(_mapper.Map<User>(command.UserDto));
                _logger.LogDebug("ready for updating!");
                if (await _unitOfWork.SaveChangesAsync() <= 0)
                {
                    return Result.Fail($"Fail to update of user {user.Email}");
                }

                return true;
            }

            //Create new user
            user = _mapper.Map<User>(command.UserDto);

            var entity = await _userRepository.Insert(user);
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return Result.Fail($"Fail to create of user {entity.Email}");
            }
            var message = "New learner: " + entity.FirstName + " " + entity.LastName + " at " +
                          entity.CreationTime.ToLongDateString();
            await _publisher.Publish(new NewObjectCreatedEvent(entity.Id, message, NotificationEnum.Learner),
                cancellationToken);
            return true;
        }
        catch (Exception ex)
        {
            //throw new Exception("Error happens when user is adding or updating." + ex.Message);
            return Result.Fail("Error happens when user is adding or updating.");

        }
    }
}