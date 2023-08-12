using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Commands;
using CED.Domain.Interfaces.Services;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Shared.NotificationConsts;
using CED.Domain.Subjects;
using CED.Domain.Users;
using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;

namespace CED.Application.Services.Users.Admin.Commands;

public class CreateUpdateTutorCommandHandler : CreateUpdateCommandHandler<CreateUpdateTutorCommand>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IRepository<TutorMajor> _tutorMajorRepository;
    private readonly ICloudinaryFile _cloudinaryFile;


    public CreateUpdateTutorCommandHandler(ITutorRepository tutorRepository, 
        IPublisher publisher,
        IRepository<TutorMajor> tutorMajorRepository,
        ICloudinaryFile cloudinaryFile,
        ILogger<CreateUpdateTutorCommandHandler> logger, IMapper mapper, IUnitOfWork unitOfWork, IAppCache cache) : base(logger,mapper, unitOfWork, cache,publisher)
    {
        _tutorRepository = tutorRepository;
        _cloudinaryFile = cloudinaryFile;
        _tutorMajorRepository = tutorMajorRepository;
    }

    public override async Task<Result<bool>> Handle(CreateUpdateTutorCommand command, CancellationToken cancellationToken)
    {
        try
        {
            
            var tutor = await _tutorRepository.GetById(command.TutorForDetailDto.Id);
            
            //Collect major ids
            var newMajorUpdateList = command.SubjectIds.DistinctBy(x => x).ToList();
            //Check if the subject existed
            if (tutor is not null)
            {
                var currentMajor = tutor.Subjects;
                // check the subject changes
                foreach (var major in currentMajor)
                {
                    if (!newMajorUpdateList.Contains(major.Id))
                    {
                        currentMajor.Remove(major);
                        _logger.LogDebug("Remove subject {0} from tutor's major", major.Name);
                    }
                    else
                    {
                        //remove the subject from newMajorUpdate
                        var removeResult = newMajorUpdateList.Remove(major.Id);
                    }
                }
                
                //Add the left subjects
                foreach (var newMu in newMajorUpdateList)
                {
                    await _tutorMajorRepository.Insert(new TutorMajor()
                    {
                        TutorId = tutor.Id,
                        SubjectId = newMu
                    });
                }


                var tutorToUpdate = _mapper.Map<Domain.Users.Tutor>(command.TutorForDetailDto);
                tutor.VerifyTutorInformation(tutorToUpdate);
                
                if (await _unitOfWork.SaveChangesAsync(cancellationToken) <= 0)
                {
                    return Result.Fail($"Fail to update of tutor {tutor.Email}" );
                }
                return true;
            }
            
            //Create new tutor
            _logger.LogDebug("ready for creating!");
            var entityToCreate = await _tutorRepository.Insert(_mapper.Map<Domain.Users.Tutor>(command.TutorForDetailDto));
            
            // add new majors to tutor
            foreach (var newMu in newMajorUpdateList)
            {
                await _tutorMajorRepository.Insert(new TutorMajor()
                {
                    TutorId = entityToCreate.Id,
                    SubjectId = newMu
                });
            }
            if (await _unitOfWork.SaveChangesAsync() <= 0)
            {
                return Result.Fail($"Fail to create of tutor {entityToCreate.Email}" );
            }
            var message = "New tutor: " + entityToCreate.FirstName + " " + entityToCreate.LastName + " at " + entityToCreate.CreationTime.ToLongDateString();
            await _publisher.Publish(new NewObjectCreatedEvent(entityToCreate.Id, message, NotificationEnum.Tutor), cancellationToken);
            return true;

        }
        catch (Exception ex)
        {
            return Result.Fail("Error happens when tutor is adding or updating: " + ex.Message);
        }
        
    }

}