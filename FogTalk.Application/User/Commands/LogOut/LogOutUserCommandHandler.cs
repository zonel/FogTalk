using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Security;
using FogTalk.Application.Security.Dto;
using FogTalk.Application.User.Commands.Authenticate;

namespace FogTalk.Application.User.Commands.LogOut;

public class LogOutUserCommandHandler: ICommandHandler<LogOutUserCommand,JwtDto>
{
    private readonly IAuthenticator _authenticator;

    public LogOutUserCommandHandler(IAuthenticator authenticator)
    {
        _authenticator = authenticator;
    }
    
    public Task<JwtDto> Handle(LogOutUserCommand request, CancellationToken cancellationToken)
    {
        _authenticator.InvalidateTokenAsync(request.jwtDto);
        return Task.FromResult(request.jwtDto);
    }
}