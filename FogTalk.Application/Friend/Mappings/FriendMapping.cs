using FogTalk.Application.Chat.Dto;
using FogTalk.Application.User.Dto;
using Mapster;

namespace FogTalk.Application.Friend.Mappings;
public class FriendMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<IEnumerable<Domain.Entities.User>, IEnumerable<ShowUserDto>>();
        config.ForType<IEnumerable<ShowUserDto>, IEnumerable<Domain.Entities.User>>();
    }
}