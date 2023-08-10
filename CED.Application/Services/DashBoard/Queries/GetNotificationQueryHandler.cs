using CED.Application.Services.Abstractions.QueryHandlers;
using CED.Contracts.Notifications;
using CED.Domain.Notifications;
using CED.Domain.Repository;
using FluentResults;
using MapsterMapper;

namespace CED.Application.Services.DashBoard.Queries;

public class GetNotificationQueryHandler : GetByIdQueryHandler<GetNotificationQuery, List<NotificationDto>>
{
    private readonly IRepository<Notification> _notificationRepository;

    public GetNotificationQueryHandler(IMapper mapper, 
        IRepository<Notification> notificationRepository
        ) :
        base(mapper)
    {
        _notificationRepository = notificationRepository;
    }

    public override async Task<Result<List<NotificationDto>>> Handle(GetNotificationQuery query,
        CancellationToken cancellationToken)
    {
        await Task.CompletedTask;

        // Create a dateListRange
        var notiList = _notificationRepository.GetAll().Where(x => x.CreationTime >= DateTime.Today).ToList();
        var notiDtoList = _mapper.Map<List<NotificationDto>>(notiList);
        return notiDtoList;
    }
}  