using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Contracts.Users;
using CED.Domain.Interfaces.Services;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using Mapster;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Tutor.Registers;

public class TutorRegisterCommandHandler : CreateUpdateCommandHandler<TutorRegistrationCommand>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationInfoRepository;
    private readonly IRepository<TutorMajor> _tutorMajorInfoRepository;
    private readonly IUserRepository _userRepository;
    private readonly ICloudinaryFile _cloudinaryFile;
    private readonly IUnitOfWork _unitOfWork;

    public TutorRegisterCommandHandler(IUserRepository userRepository,
        IRepository<TutorVerificationInfo> tutorVerificationInfoRepository,
        IRepository<TutorMajor> tutorMajorInfoRepository,
        ICloudinaryFile cloudinaryFile,
        ITutorRepository tutorRepository,
        IUnitOfWork unitOfWork,
        ILogger<TutorRegisterCommandHandler> logger,
        IMapper mapper) : base(logger, mapper)
    {
        _userRepository = userRepository;
        _tutorRepository = tutorRepository;
        _unitOfWork = unitOfWork;
        _tutorMajorInfoRepository = tutorMajorInfoRepository;
        _cloudinaryFile = cloudinaryFile;
        _tutorVerificationInfoRepository = tutorVerificationInfoRepository;
    }

    public override async Task<bool> Handle(TutorRegistrationCommand command, CancellationToken cancellationToken)
    {
        try
        {
            //Check if the user existed
            _logger.LogDebug("Getting user info");
            var tutor = (command.TutorDto).Adapt<Domain.Users.Tutor>();

            var user = await _userRepository.GetUserByEmail(email: command.TutorDto.Email);
            if (user is null)
            {
                throw new Exception("User with an email doesn't exist / User may be already a tutor.");
            }

            user.Role = UserRole.Tutor;
            _userRepository.Update(user);


            _logger.LogDebug("Creating new tutor profile...");
            //tutor.Id = user.Id;
            //Set to false bc tutor will need to be verified by user
            tutor.IsVerified = false;
            await _tutorRepository.Insert(tutor);

            //await _unitOfWork.SaveChangesAsync(cancellationToken);
            //Handle filepath !!! Upgrade
            if (command.FilePaths is { Count: > 0 })
            {
                var verifications = _tutorVerificationInfoRepository
                    .GetAll().Where(x => x.TutorId.Equals(tutor.Id)).ToList();
                foreach (var i in verifications)
                {
                    _tutorVerificationInfoRepository.Delete(i);
                }


                //verifications = new List<TutorVerificationInfo>();
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
                //verifications = new List<TutorVerificationInfo>();
                foreach (var i in command.SubjectIds)
                {
                    await _tutorMajorInfoRepository.Insert(new TutorMajor()
                    {
                        SubjectId = new Guid(i),
                        TutorId = tutor.Id
                    });
                }
            }

            _logger.LogDebug("Done.");
            return true;
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message);
            return false;
        }
    }
}