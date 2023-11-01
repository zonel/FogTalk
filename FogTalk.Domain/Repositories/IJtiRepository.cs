namespace FogTalk.Domain.Repositories;

public interface IJtiRepository
{
    public bool IsJtiBlacklisted(string jti);
    public void AddJtiToBlacklist(string jti);
    public string ExtractJtiFromToken(string jwtToken);
}