using FogTalk.Application.Security;
using FogTalk.Domain.Repositories;
using Microsoft.AspNetCore.Authorization;
    
namespace FogTalk.Infrastructure.Security;

public class JtiAuthorizationHandler : AuthorizationHandler<JtiRequirement>
{
    private readonly IJtiRepository _jtiRepository;

    public JtiAuthorizationHandler(IJtiRepository jtiRepository)
    {
        _jtiRepository = jtiRepository;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, JtiRequirement requirement)
    {
        var httpContext = context.Resource as Microsoft.AspNetCore.Http.DefaultHttpContext;
        
        if (httpContext.Request.Headers.TryGetValue("Authorization", out var authHeader))
        {
            var token = authHeader.ToString().Replace("Bearer ", string.Empty);
            var jti = _jtiRepository.ExtractJtiFromToken(token);
            var jwtBlacklistedAlready = await _jtiRepository.IsJtiBlacklistedAsync(jti, CancellationToken.None);
            
            if (!string.IsNullOrEmpty(jti) && !jwtBlacklistedAlready)
            {
                context.Succeed(requirement);
            }
        }
    }
}