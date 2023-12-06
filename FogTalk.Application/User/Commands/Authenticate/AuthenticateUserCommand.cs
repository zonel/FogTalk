using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Security.Dto;
using FogTalk.Application.User.Dto;

namespace FogTalk.Application.User.Commands.Authenticate;

public record AuthenticateUserCommand(LoginUserDto loginUserDto, CancellationToken Token) : ICommand<JwtDto>;
