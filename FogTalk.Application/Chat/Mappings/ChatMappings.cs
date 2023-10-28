using FogTalk.Application.Chat.Dto;
using Mapster;

namespace FogTalk.Application.Chat.Mappings;
public class ChatMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<ChatDto, Domain.Entities.Chat>()
            .Map(dest => dest.Name, src => src.Name)
            .Map(dest => dest.IsGroupChat, src => src.IsGroupChat);
    }
}