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
        IMapper mapper, IAppCache cache, IPublisher publisher) : base(logger, mapper, unitOfWork, cache, publisher)
    {
        _userRepository = userRepository;
        _tutorRepository = tutorRepository;
        _tutorMajorInfoRepository = tutorMajorInfoRepository;
        _cloudinaryFile = cloudinaryFile;
        _tutorVerificationInfoRepository = tutorVerificationInfoRepository;
    }

    public override async Task<Result<bool>> Handle(TutorRegistrationCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            //Check if the user existed
            //TODO: Check if the logic goes well

            var user = await _userRepository.GetById(command.TutorForRegistrationDto.Id);

            if (user is null)
            {
                return Result.Fail(new NonExistUserError());
            }
            //TODO: need to mapper from dto
            var tutor = _mapper.Map<Domain.Users.Tutor>(command.TutorForRegistrationDto);
            tutor.Role = UserRole.Tutor;
            tutor.Description = command.TutorForRegistrationDto.Description;
            tutor.University = command.TutorForRegistrationDto.University;
            tutor.AcademicLevel = command.TutorForRegistrationDto.AcademicLevel;
            tutor.IsVerified = false;

             _userRepository.Delete(user);
            var tutorToRegister = await _tutorRepository.Insert(tutor); 

            

            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return Result.Fail("Fail to register new tutor");
            }
            
            foreach (var i in command.TutorForRegistrationDto.TutorVerificationInfoDtos)
            {
                i.Image = _cloudinaryFile.UploadImage(i.Image);
                var tutorVerificationInfo = _mapper.Map<TutorVerificationInfo>(i);
                await _tutorVerificationInfoRepository.Insert(tutorVerificationInfo);
            }

            //Handle major
            if (command.TutorForRegistrationDto.Majors is { Count: > 0 })
            {
                foreach (var i in command.TutorForRegistrationDto.Majors)
                {
                    await _tutorMajorInfoRepository.Insert(new TutorMajor()
                    {
                        SubjectId = new Guid(i),
                        TutorId = tutorToRegister.Id
                    });
                }
            }


            if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
            {
                return Result.Fail("Fail to save majors and verification info");
            }

            _logger.LogInformation("Done");
            return true;
        }
        catch (Exception e)
        {
            return Result.Fail("Error while registering tutor. Details error: " + e.Message);
        }
    }
}