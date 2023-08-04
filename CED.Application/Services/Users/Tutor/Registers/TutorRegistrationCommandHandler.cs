using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Contracts.Users;
using CED.Domain.Interfaces.Services;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using FluentResults;
using LazyCache;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Tutor.Registers;

public class TutorRegisterCommandHandler : CreateUpdateCommandHandler<TutorRegistrationCommand>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationInfoRepository;
    private readonly IRepository<TutorMajor> _tutorMajorInfoRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICloudinaryFile _cloudinaryFile;

    public TutorRegisterCommandHandler(IUserRepository userRepository,
        IRepository<TutorVerificationInfo> tutorVerificationInfoRepository,
        IRepository<TutorMajor> tutorMajorInfoRepository,
        ICloudinaryFile cloudinaryFile,
        ITutorRepository tutorRepository,
        IUnitOfWork unitOfWork,
        ILogger<TutorRegisterCommandHandler> logger,
        IMapper mapper, IAppCache cache, IPublisher publisher) : base(logger, mapper,unitOfWork,cache,publisher)
    {
        _userRepository = userRepository;
        _tutorRepository = tutorRepository;
        _tutorMajorInfoRepository = tutorMajorInfoRepository;
        _cloudinaryFile = cloudinaryFile;
        _tutorVerificationInfoRepository = tutorVerificationInfoRepository;
    }

    public override async Task<Result<bool>> Handle(TutorRegistrationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            //Check if the user existed
            //TODO: Check if the logic goes well
            
            var user = await _userRepository.ExistenceCheck(command.TutorDto.Id);
            
            if (user is null)
            {
                return Result.Fail(new NonExistUserError());
            }
            var tutor =  _mapper.Map<Domain.Users.Tutor>(command.TutorDto);

            //user.UpdateUserInformationExceptImage(updateUser);
            tutor.Role = UserRole.Tutor;
            //user.CreationTime = DateTime.Now;

            //Set to false bc tutor will need to be verified by user
            tutor.IsVerified = false;
            
            //Handle filepath !!! Upgrade
            //alternative flows: get the verification by id then push into verification list of tutor
            if (command.FilePaths is { Count: > 0 })
            {
                foreach (var i in command.FilePaths)
                {
                    await _tutorVerificationInfoRepository.Insert(new TutorVerificationInfo
                    {
                        Image = _cloudinaryFile.UploadImage(i),
                        TutorId = tutor.Id
                    });
                }
            }

            //Handle major
            if (command.SubjectIds is { Count: > 0 })
            {
                foreach (var i in command.SubjectIds)
                {
                    await _tutorMajorInfoRepository.Insert(new TutorMajor()
                    {
                        SubjectId = new Guid(i),
                        TutorId = tutor.Id
                    });
                }
            }
            
            await _tutorRepository.Insert(tutor); //TODO: this will be fail

            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return Result.Fail("Fail to register new tutor");
            }
            _logger.LogDebug("Done.");
            return true;
        }
        catch (Exception e)
        {
            return Result.Fail("Error while registering tutor. Details error: " + e.Message);
        }
    }
}