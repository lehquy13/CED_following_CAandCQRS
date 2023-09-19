using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Queries;
using CED.Application.Services.ClassInformations.Queries.GetAllClassInformationsQuery;
using CED.Domain.ClassInformations;
using CED.Domain.Repository;
using CED.Domain.Shared.ClassInformationConsts;
using CED.Domain.Shared.NotificationConsts;
using CED.Domain.Users;
using FluentResults;
using LazyCache;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace CED.Application.Services.ClassInformations.Commands;

public class CreateUpdateClassInformationCommandHandler
    : CreateUpdateCommandHandler<CreateUpdateClassInformationCommand>
{
    private readonly IClassInformationRepository _classInformationRepository;
    private readonly IUserRepository _userRepository;

    public CreateUpdateClassInformationCommandHandler(
        IClassInformationRepository classInformationRepository,
        IUnitOfWork unitOfWork,
        IAppCache cache,
        IPublisher publisher,
        ILogger<CreateUpdateClassInformationCommandHandler> logger, IMapper mapper, IUserRepository userRepository)
        : base(logger, mapper, unitOfWork,cache,publisher)
    {
        _classInformationRepository = classInformationRepository;
        _userRepository = userRepository;
    }

    public override async Task<Result<bool>> Handle(CreateUpdateClassInformationCommand command,
        CancellationToken cancellationToken)
    {
            try
            {
                var classInformation = await _classInformationRepository.GetById(command.ClassInformationDto.Id);

                //Check if the class existed
                if (classInformation is not null)
                {
                    classInformation = _mapper.Map<ClassInformation>(command.ClassInformationDto);

                    //update last modification time
                    classInformation.LastModificationTime = DateTime.Now;
                    if (classInformation.TutorId != null)
                    {
                        classInformation.Status = Status.Confirmed;
                    }

                    //Update existed class
                    var requestGettingClassesFromDb =
                        (await _classInformationRepository
                            .GetRequestGettingClassesByClassId(classInformation
                                .Id)) // get all request getting classes by class ObjectId
                        .Where(x => x.TutorId != classInformation.TutorId); // get all other request in order to cancel them
                    // Cancel them 
                    foreach (var iClass in requestGettingClassesFromDb)
                    {
                        iClass.RequestStatus = RequestStatus.Canceled;
                    }

                    if (await _unitOfWork.SaveChangesAsync() > 0)
                    {
                        return true;
                    }

                    return Result.Fail<bool>("Update class and it's requests failed.");
                }

                //Create new Class
                classInformation = _mapper.Map<ClassInformation>(command.ClassInformationDto);

                if (!string.IsNullOrWhiteSpace(command.Email))
                {
                    //Class was created by a system user
                    var user = await _userRepository.GetUserByEmail(command.Email);
                    if (user != null)
                    {
                        classInformation.LearnerId = user.Id;
                    }
                }

                //update last modification time
                classInformation.LastModificationTime = DateTime.Now;
                //update creation time bc it is new record
                classInformation.CreationTime = DateTime.Now;

                //Handle publish event to notification service
                var entity = await _classInformationRepository.Insert(classInformation);
                if (!(await _unitOfWork.SaveChangesAsync() > 0))
                {
                    return Result.Fail("Fail to save changes while creating new class.");
                }
                var message = "New class: " + entity.Title + " at " + entity.CreationTime.ToLongDateString();
                await _publisher.Publish(
                    new NewObjectCreatedEvent(entity.Id, message, NotificationEnum.ClassInformation),
                    cancellationToken);

                // Clear cache
                var defaultRequest = new GetAllClassInformationsQuery();
                _cache.Remove(defaultRequest.GetType() + JsonConvert.SerializeObject(defaultRequest));
                return Result.Ok(true);
            }
            catch (Exception ex)
            {
                return Result.Fail("Error happens when class is adding or updating." + ex.Message);
            }
    }
}