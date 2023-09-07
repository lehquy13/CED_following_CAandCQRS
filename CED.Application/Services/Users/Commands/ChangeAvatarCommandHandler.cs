using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Interfaces.Services;
using CED.Domain.Repository;
using CED.Domain.Users;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Commands;

public class ChangeAvatarCommandHandler : IRequestHandler<ChangeAvatarCommand, Result<string>>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<ChangeAvatarCommandHandler> _logger;
    private readonly ICloudinaryFile _cloudinaryFile;
    private readonly IMapper _mapper;
    private readonly IPublisher _publisher;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IAppCache _cache;

    public ChangeAvatarCommandHandler(IUserRepository userRepository,
        ILogger<ChangeAvatarCommandHandler> logger,
        ICloudinaryFile cloudinaryFile,
        IMapper mapper, IPublisher publisher, IUnitOfWork unitOfWork, IAppCache cache) 
    {
        _userRepository = userRepository;
        _logger = logger;
        _cloudinaryFile = cloudinaryFile;
        _mapper = mapper;
        _publisher = publisher;
        _unitOfWork = unitOfWork;
        _cache = cache;
    }

    public async Task<Result<string>> Handle(ChangeAvatarCommand command, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetById(new Guid(command.Id));
        //Check if the subject existed
        if (user is not null && command.File != null)
        {
            if (command.File.Length > 0)
            {
                ImageUploadResult newImageResult;
                await using (var stream = command.File.OpenReadStream())
                {
                    var result = _cloudinaryFile.UploadImage(command.File.FileName, stream);

                    user.Image = result;

                    if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
                    {
                        return Result.Fail($"Fail to update of user {user.Email}");
                    }

                    return result;
                }
            }
        }
        return Result.Fail("Fail to update avatar");
    }
}