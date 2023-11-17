using FogTalk.Application.Chat.Dto;
using FogTalk.Application.Friend.Dto;
using FogTalk.Application.User.Dto;
using Mapster;

namespace FogTalk.Application.Friend.Mappings;
public class FriendMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Domain.Entities.User, ShowFriendDto>()
            .Map(dest => dest, src => src.UserName)
            .Map(dest => dest.Bio, src => src.Bio)
            .Map(dest => dest.ProfilePicture, src => src.ProfilePicture);
    }
}