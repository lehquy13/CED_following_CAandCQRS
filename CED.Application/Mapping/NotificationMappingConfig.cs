using CED.Contracts.Notifications;
using CED.Domain.Notifications;
using Mapster;

namespace CED.Application.Mapping;

public class NotificationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<LearnerDto, TutorForDetailDto>();

        config.NewConfig<Notification,NotificationDto>()
            .Map(des => des.DetailPath, src => $"/{src.NotificationType.ToString()}/Detail?ObjectId={src.ObjectId}")
            .Map(des => des, src => src);
    }
}