using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using FogTalk.Application.Security;
using FogTalk.Application.Security.Dto;
using FogTalk.Domain.Exceptions;
using FogTalk.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace FogTalk.Infrastructure.Auth;

public class Authenticator : IAuthenticator
{
    private readonly DateTime _clock;
    private readonly string _issuer;
    private readonly TimeSpan _expiry;
    private readonly string _audience;
    private readonly SigningCredentials _signingCredentials;
    private readonly JwtSecurityTokenHandler _jwtSecurityToken = new JwtSecurityTokenHandler();
    private readonly IJtiRepository _jtiRepository;

    public Authenticator(IOptions<AuthOptions> options, IJtiRepository jtiRepository)
    {
        _clock = DateTime.Now;
        _issuer = options.Value.Issuer;
        _audience = options.Value.Audience;
        _expiry = options.Value.Expiry ?? TimeSpan.FromHours(1);
        _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(options.Value.SigningKey)),
            SecurityAlgorithms.HmacSha256);

        _jtiRepository = jtiRepository;
    }
    
    public JwtDto CreateToken(int userId)
    {
        var now = DateTime.Now;
        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sid, userId.ToString()),
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var expires = now.Add(_expiry);
        var jwt = new JwtSecurityToken(_issuer, _audience, claims, now, expires, _signingCredentials);
        var token = _jwtSecurityToken.WriteToken(jwt);

        return new JwtDto
        {
            AccessToken = token
        };
    }

    //TODO: change to async
    public async void InvalidateTokenAsync(JwtDto token)
    {
        var jti = _jtiRepository.ExtractJtiFromToken(token.AccessToken);
        var jwtBlacklistedAlready = await _jtiRepository.IsJtiBlacklistedAsync(jti);
        
        if (jwtBlacklistedAlready)
        {
            throw new TokenAlreadyBlacklistedException("Token already blacklisted.");
        }
        else
        {
            // Adds token jti claim to blacklist.
            _jtiRepository.AddJtiToBlacklist(jti);
        }
    }
}