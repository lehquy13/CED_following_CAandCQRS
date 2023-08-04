using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Interfaces.Services;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Student.Commands;

public class LearnerInfoChangingCommandHandler : CreateUpdateCommandHandler<LearnerInfoChangingCommand>
{
    private readonly IUserRepository _userRepository; 
    private readonly ICloudinaryFile _cloudinaryFile;

    public LearnerInfoChangingCommandHandler(IUserRepository userRepository,
        ILogger<LearnerInfoChangingCommandHandler> logger,
        ICloudinaryFile cloudinaryFile,
        IMapper mapper, IPublisher publisher, IUnitOfWork unitOfWork, IAppCache cache) : base(logger,mapper,unitOfWork,cache, publisher)
    {
        _userRepository = userRepository;
        _cloudinaryFile = cloudinaryFile;
    }
    public override async Task<Result<bool>> Handle(LearnerInfoChangingCommand command,  CancellationToken cancellationToken) 
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
            
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return Result.Fail($"Fail to update of user {user.Email}");
            }    
            return true;
        }
        _logger.LogDebug("ready for creating!");

        user = _mapper.Map<User>(command.LearnerDto);

        var entity = await _userRepository.Insert(user);
        if (await _unitOfWork.SaveChangesAsync() <= 0)
        {
            return Result.Fail($"Fail to create of user {entity.Email}");
        } 
        return true;
    }
}

