using FogTalk.Application.Security.Dto;

namespace FogTalk.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateTokenAsync(int userId, CancellationToken token);
    void InvalidateTokenAsync(JwtDto token, CancellationToken cancellationToken);
}