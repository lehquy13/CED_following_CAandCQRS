using CED.Domain.Interfaces.Logger;
using CED.Domain.Notifications;
using CED.Domain.Repository;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

internal class NewObjectCreatedEventHandler : INotificationHandler<NewObjectCreatedEvent>
{
    private readonly IRepository<Notification> _notificationRepository;
    private readonly IAppLogger<NewObjectCreatedEventHandler> _logger;

    public NewObjectCreatedEventHandler(IRepository<Notification> notificationRepository, IAppLogger<NewObjectCreatedEventHandler> logger)
    {
        _notificationRepository = notificationRepository;
        _logger = logger;
    }

    public async Task Handle(NewObjectCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogDebug("Creating new notification...");

        var entity = await _notificationRepository.Insert(new Notification()
        {
            Message = notification.Message,
            ObjectId = notification.ObjectId,
            NotificationType = notification.NotificationEnum,
            CreationTime = DateTime.Now,
            LastModificationTime = DateTime.Now,
        });
        if(entity != null)
        {
            _logger.LogDebug("Created new notification...");
        }
        else
        {
            _logger.LogError("Error when creating notification");
        }
    }
}

