using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Security.Dto;

namespace FogTalk.Application.User.Commands.LogOut;

public record LogOutUserCommand(JwtDto jwtDto) : ICommand<JwtDto>
{
}
