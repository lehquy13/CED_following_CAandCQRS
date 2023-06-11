using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Users;
using MapsterMapper;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Tutor.Registers;

public class TutorRegisterCommandHandler : CreateUpdateCommandHandler<TutorRegisterCommand>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IRepository<TutorVerificationInfo> _tutorVerificationInfoRepository;
    private readonly IUserRepository _userRepository;
    public TutorRegisterCommandHandler(IUserRepository userRepository,IRepository<TutorVerificationInfo> tutorVerificationInfoRepository
        ,ITutorRepository tutorRepository, ILogger<TutorRegisterCommandHandler> logger,IMapper mapper) : base(logger,mapper)
    {
        _userRepository = userRepository;
       _tutorRepository = tutorRepository;
        _tutorVerificationInfoRepository = tutorVerificationInfoRepository;
    }
    public override async Task<bool> Handle(TutorRegisterCommand command, CancellationToken cancellationToken)
    {
        try
        {
            //Check if the user existed
            _logger.LogDebug("Getting user info");
            var user = await _userRepository.GetUserByEmail(command.TutorDto.Email);
            if (user is null || user.Role == UserRole.Tutor)
            {
                throw new Exception("User with an email doesn't exist / User may be already a tutor.");
            }
            _logger.LogDebug("Creating new tutor profile...");
            var tutor = _mapper.Map<Domain.Users.Tutor>(command.TutorDto);
            //Set to false bc tutor will need to be verified by user
            tutor.IsVerified = false;
            await _tutorRepository.Insert(tutor);
            var tutorVerificationInfos =
                _mapper.Map<List<TutorVerificationInfo>>(command.TutorDto.TutorVerificationInfoDtos);
            foreach (var tutorVerification in tutorVerificationInfos)
            {
                await _tutorVerificationInfoRepository.Insert(tutorVerification);
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

