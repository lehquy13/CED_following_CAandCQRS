using CED.Application.Common.Errors.ClassInformations;
using CED.Application.Common.Errors.Users;
using CED.Application.Services.Abstractions.CommandHandlers;
using CED.Application.Services.ClassInformations.Commands;
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

namespace CED.Application.Services.ClassInformations.Tutor.Commands.ApplyClass;

public class RequestGettingClassCommandHandler : CreateUpdateCommandHandler<RequestGettingClassCommand>
{
    private readonly ITutorRepository _tutorRepository;
    private readonly IRequestGettingClassRepository _requestGettingClassRepository;
    private readonly IClassInformationRepository _classInformationRepository;

    public RequestGettingClassCommandHandler(
        ITutorRepository tutorRepository,
        IClassInformationRepository classInformationRepository,
        ILogger<RequestGettingClassCommandHandler> logger,
        IRequestGettingClassRepository requestGettingClassRepository,
        IUnitOfWork unitOfWork,
        IAppCache cache,
        IMapper mapper,
        IPublisher publisher) : base(logger, mapper, unitOfWork, cache, publisher)
    {
        _classInformationRepository = classInformationRepository;
        _tutorRepository = tutorRepository;
        _requestGettingClassRepository = requestGettingClassRepository;
    }

    public override async Task<Result<bool>> Handle(RequestGettingClassCommand command,
        CancellationToken cancellationToken)
    {
        try
        {
            
            //Check if the user existed
            var tutor = await _tutorRepository.ExistenceCheck(command.TutorId);
            if (tutor is null)
            {
                return Result.Fail<bool>(new NonExistTutorError());
            }

            var classInfor = await _classInformationRepository.GetById(command.ClassId);
            if (classInfor is null)
            {
                return Result.Fail<bool>(new NonExistClassError());
            }

            if (classInfor.IsDeleted is true || !classInfor.Status.Equals(Status.Available))
            {
                return Result.Fail<bool>(new UnAvailableClassError());
            }

       
            if ((await _requestGettingClassRepository.IsRequested(command.ClassId, command.TutorId)))
            {
                _logger.LogError("Tutor has already requested");
                return Result.Fail<bool>(new RequestedClassError());
            }

            //Create new request
            var request = new RequestGettingClass()
            {
                ClassInformationId = classInfor.Id,
                TutorId = tutor.Id
            };

            var entity = await _requestGettingClassRepository.Insert(request);
            if (await _unitOfWork.SaveChangesAsync(cancellationToken) > 0)
            {
                var message = "New request: " + classInfor.Title + " at " + DateTime.Now.ToLongDateString();
                await _publisher.Publish(
                    new NewObjectCreatedEvent(entity.ClassInformationId, message, NotificationEnum.RequestGettingClass),
                    cancellationToken);
                return true;
            }
            return Result.Fail("Fail to create new request");

        }
        catch (Exception e)
        {
            _logger.LogError("{0}" ,e.Message);
            return Result.Fail(e.Message);
        }
    }
}