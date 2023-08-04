using CED.Domain.Interfaces.Logger;
using CED.Domain.Notifications;
using CED.Domain.Repository;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

internal class NewObjectCreatedEventHandler : INotificationHandler<NewObjectCreatedEvent>
{
    private readonly IRepository<Notification> _notificationRepository;
    private readonly IAppLogger<NewObjectCreatedEventHandler> _logger;
    private readonly IUnitOfWork _unitOfWork;
    public NewObjectCreatedEventHandler(IRepository<Notification> notificationRepository, IAppLogger<NewObjectCreatedEventHandler> logger, IUnitOfWork unitOfWork)
    {
        _notificationRepository = notificationRepository;
        _logger = logger;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(NewObjectCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Creating new notification...");

        var entityToCreate = new Notification()
        {
            Message = notification.Message,
            ObjectId = notification.ObjectId,
            NotificationType = notification.NotificationEnum,
            CreationTime = DateTime.Now,
            LastModificationTime = DateTime.Now,
        };
        
        await _notificationRepository.Insert(entityToCreate);
        
        if( await _unitOfWork.SaveChangesAsync(cancellationToken) > 0)
        {
            _logger.LogDebug("Created new notification");
        }
        else
        {
            _logger.LogError("Fail to add new notification");
        }
    }
}

