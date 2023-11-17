using FogTalk.Application.Friend.Dto;
using FogTalk.Application.Notification.Dto;
using Mapster;

namespace FogTalk.Application.Notification.Mappings;

public class NotificationMapping
{
    public class NotificationMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<Domain.Entities.Notification, NotificationDto>()
                .Map(dest => dest.Message, src => src.Message)
                .Map(dest => dest.CreatedAt, src => src.CreatedAt)
                .Map(dest => dest.IsRead, src => src.IsRead);
        }
    }
}