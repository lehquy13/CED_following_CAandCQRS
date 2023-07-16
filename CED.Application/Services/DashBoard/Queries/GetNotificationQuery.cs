using CED.Contracts.Charts;
using CED.Contracts.Notifications;
using MediatR;

namespace CED.Application.Services.DashBoard.Queries;

public record GetNotificationQuery
(
   
): IRequest<List<NotificationDto>>;