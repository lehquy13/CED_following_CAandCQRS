using CED.Contracts.Notifications;
using CED.Domain.Notifications;
using Mapster;

namespace CED.Application.Mapping;

public class NotificationMappingConfig : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        //config.NewConfig<LearnerDto, TutorDto>();

        config.NewConfig<Notification,NotificationDto>()
            .Map(des => des.DetailPath, src => $"/{src.NotificationType.ToString()}/Detail?id={src.ObjectId}")
            .Map(des => des, src => src);
    }
}