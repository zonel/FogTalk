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

namespace FogTalk.Infrastructure.ServicesConfiguration;

public static class ServiceCollection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<FogTalkDbContext>(options =>
        {
            options.UseSqlServer(Environment.GetEnvironmentVariable("FOGTALK_CONNECTION_STRING"));
        });
        services.AddScoped(typeof(IGenericRepository<,>), typeof(GenericRepository<,>));
        services.AddScoped<IAuthenticator, Authenticator>(); // Register the Authenticator
        services.AddScoped<IPasswordManager, PasswordManager>(); // Register the PasswordManager
        services.AddScoped<IUserRepository, UserRepository>(); // Register the UserRepository
        services.AddSingleton<IPasswordHasher<User>, PasswordHasher<User>>();
        services.AddScoped<IDatabaseSeeder, DatabaseSeeder>(); // Register the DatabaseSeeder
        
        return services;
    }
}
