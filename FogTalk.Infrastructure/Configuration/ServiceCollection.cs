using System.Text;
using FogTalk.Application.Abstraction;
using FogTalk.Application.Security;
using FogTalk.Domain.Entities;
using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Auth;
using FogTalk.Infrastructure.Persistence;
using FogTalk.Infrastructure.Repository;
using FogTalk.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace FogTalk.Infrastructure.ServicesConfiguration;

public static class ServiceCollection
{
    private const string AuthOptionsSectionName = "AuthOptions";
    
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<AuthOptions>(configuration.GetRequiredSection(AuthOptionsSectionName));
        services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            o.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        });
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                var authOptions = new AuthOptions()
                {
                    Audience = configuration.GetSection(AuthOptionsSectionName).GetSection("Audience").Value,
                    Issuer = configuration.GetSection(AuthOptionsSectionName).GetSection("Issuer").Value,
                    SigningKey = configuration.GetSection(AuthOptionsSectionName).GetSection("SigningKey").Value,
                    Expiry = TimeSpan.FromHours(1)
                };


                options.Audience = authOptions.Audience;
                options.IncludeErrorDetails = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = authOptions.Issuer,
                    ClockSkew = TimeSpan.Zero,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(authOptions.SigningKey))
                };
            });
        
        services.AddDbContext<FogTalkDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("FOGTALK_CONNECTION_STRING"));
        }).AddScoped<FogTalkDbContext>();
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IAuthenticator, Authenticator>(); // Register the Authenticator
        services.AddScoped<IPasswordManager, PasswordManager>(); // Register the PasswordManager
        services.AddScoped<IUserRepository, UserRepository>(); // Register the UserRepository
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IDatabaseSeeder, DatabaseSeeder>(); // Register the DatabaseSeeder
        services.AddScoped<IJtiRepository, JtiRepository>(); // Register the JtiRepository
        services.AddScoped<IChatRepository, ChatRepository>(); // Register the ChatRepository
        services.AddScoped<IMessageRepository, MessageRepository>(); // Register the MessageRepository
        services.AddScoped<IFriendRepository, FriendRepository>(); // Register the FriendRepository
        services.AddScoped<INotificationRepository, NotificationRepository>(); // Register the NotificationRepository
        services.AddScoped<IUserSearchRepository, UserSearchRepository>(); // Register the UserSearchRepository
        
        
        services.AddScoped<IAuthorizationHandler, JtiAuthorizationHandler>(); // Register the JtiAuthorizationHandler
        services.AddAuthorization(options =>
        {
            options.AddPolicy("JtiPolicy", policy =>
            {
                policy.Requirements.Add(new JtiRequirement());
            });
        });
        
        return services;
    }
}
