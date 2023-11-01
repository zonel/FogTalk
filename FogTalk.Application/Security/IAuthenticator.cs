using FogTalk.Application.Security.Dto;

namespace FogTalk.Application.Security;

public interface IAuthenticator
{
    JwtDto CreateToken(int userId);
    void InvalidateTokenAsync(JwtDto token);
}