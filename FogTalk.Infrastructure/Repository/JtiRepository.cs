using System.IdentityModel.Tokens.Jwt;
using FogTalk.Domain.Entities;
using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace FogTalk.Infrastructure.Repository;

public class JtiRepository : IJtiRepository
{
    private readonly FogTalkDbContext _dbContext;

    public JtiRepository(FogTalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public async Task<bool> IsJtiBlacklistedAsync(string jti, CancellationToken cancellationToken)
    {
        var jtiEntity = await _dbContext.Jtis.SingleOrDefaultAsync(j => j.JtiValue == jti, cancellationToken);
        return jtiEntity != null;
    }

    public void AddJtiToBlacklist(string jti)
    {
            var newJtiToBlacklist = new Jti()
            {
                JtiValue = jti,
                BlacklistedAt = DateTime.Now
            };
        
             _dbContext.Jtis.Add(newJtiToBlacklist);
             _dbContext.SaveChanges();
    }

    public string ExtractJtiFromToken(string jwtToken)
    {
        var token = new JwtSecurityTokenHandler().ReadJwtToken(jwtToken);
        return token.Claims.First(c => c.Type == JwtRegisteredClaimNames.Jti).Value;
    }
}