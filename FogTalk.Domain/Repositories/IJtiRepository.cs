namespace FogTalk.Domain.Repositories;

public interface IJtiRepository
{
    public Task<bool> IsJtiBlacklistedAsync(string jti, CancellationToken cancellationToken);
    public void AddJtiToBlacklist(string jti);
    public string ExtractJtiFromToken(string jwtToken);
}