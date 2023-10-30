using FogTalk.Application.Chat.Dto;
using FogTalk.Application.User.Dto;
using Mapster;

namespace FogTalk.Application.User.Mapping;

public class UserMappings : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.ForType<Domain.Entities.User, RegisterUserDto>()
            .Map(dest => dest.UserName, src => src.UserName)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Bio, src => src.Bio)
            .Map(dest => dest.ProfilePicture, src => src.ProfilePicture);
            
        
        config.ForType<RegisterUserDto, Domain.Entities.User>()
            .Map(dest => dest.UserName, src => src.UserName)
            .Map(dest => dest.Password, src => src.Password)
            .Map(dest => dest.Email, src => src.Email)
            .Map(dest => dest.Bio, src => src.Bio)
            .Map(dest => dest.ProfilePicture, src => src.ProfilePicture);
    }
}