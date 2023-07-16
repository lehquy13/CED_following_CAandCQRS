using CED.Domain.Shared.NotificationConsts;
using MediatR;

namespace CED.Application.Services.ClassInformations.Commands;

internal record NewObjectCreatedEvent(Guid ObjectId, string Message, NotificationEnum NotificationEnum) : INotification;

