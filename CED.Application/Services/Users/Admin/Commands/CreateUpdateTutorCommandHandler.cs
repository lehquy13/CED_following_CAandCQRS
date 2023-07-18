﻿using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Domain.Interfaces.Services;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Shared.NotificationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Admin.Commands;

public class CreateUpdateTutorCommandHandler : CreateUpdateCommandHandler<CreateUpdateTutorCommand>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IUserRepository _userRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly ICloudinaryFile _cloudinaryFile;
    private readonly IPublisher _publisher;


    public CreateUpdateTutorCommandHandler(ITutorRepository tutorRepository, IUserRepository userRepository, IPublisher publisher,
        IRepository<TutorMajor> tutorMajorRepository,
        ICloudinaryFile cloudinaryFile,
        ILogger<CreateUpdateTutorCommandHandler> logger, IMapper mapper) : base(logger,mapper)
    {
        _tutorRepository = tutorRepository;
        _userRepository = userRepository;
        _cloudinaryFile = cloudinaryFile;
        _publisher = publisher;
        _tutorMajorRepository = tutorMajorRepository;
    }

    public override async Task<bool> Handle(CreateUpdateTutorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            var tutor = await _tutorRepository.GetUserByEmail(command.TutorDto.Email);
            var tutorAsUser = await _userRepository.GetUserByEmail(command.TutorDto.Email);
            var newMajorUpdate = command.SubjectIds.DistinctBy(x => x).ToList();
            //Check if the subject existed
            if (tutor is not null && tutorAsUser is not null && tutorAsUser.Role == UserRole.Tutor)
            {
                var currentMajor = _tutorMajorRepository.GetAll().Where(x => x.TutorId.Equals(command.TutorDto.Id)).ToList();
                // check the subject changes
                foreach (var major in currentMajor)
                {
                    if (!newMajorUpdate.Contains(major.SubjectId))
                    {
                        _tutorMajorRepository.Delete(major);
                        _logger.LogDebug("Remove subject {0} from tutor's major", major.SubjectId);
                    }
                    else
                    {
                        var removeResult = newMajorUpdate.Remove(major.SubjectId);
                        if (removeResult)
                        {
                            _logger.LogDebug("Remove subject {0} from newMajorUpdateList", major.SubjectId);
                        }
                        else
                        {
                            _logger.LogError("fail to remove the subject {0} from newMajorUpdateList", major.SubjectId);
                        }
                    }
                }

                foreach (var newMu in newMajorUpdate)
                {
                    await _tutorMajorRepository.Insert(new TutorMajor()
                    {
                        TutorId = tutor.Id,
                        SubjectId = newMu
                    });
                }
                
                
                tutor.VerifyTutorInformation(_mapper.Map<Domain.Users.Tutor>(command.TutorDto));
                tutorAsUser.UpdateUserInformationExceptImage(_mapper.Map<User>(command.TutorDto));
                
                _logger.LogDebug("ready for updating!");
                _tutorRepository.Update(tutor);
                _userRepository.Update(tutorAsUser);

                return true;
            }

            _logger.LogDebug("ready for creating!");
            tutorAsUser = _mapper.Map<User>(command.TutorDto);
            var entity = await _userRepository.Insert(tutorAsUser);
           
            tutor = _mapper.Map<Domain.Users.Tutor>(command.TutorDto);
            await _tutorRepository.Insert(tutor);

            // add new subjects

            foreach (var newMu in newMajorUpdate)
            {
                await _tutorMajorRepository.Insert(new TutorMajor()
                {
                    TutorId = tutor.Id,
                    SubjectId = newMu
                });
            }
            var message = "New tutor: " + entity.FirstName + " " + entity.LastName + " at " + entity.CreationTime.ToLongDateString();
            await _publisher.Publish(new NewObjectCreatedEvent(entity.Id, message, NotificationEnum.Tutor), cancellationToken);
            return true;

        }
        catch (Exception ex)
        {
            throw new Exception("Error happens when user is adding or updating." + ex.Message);
        }
        
    }

}