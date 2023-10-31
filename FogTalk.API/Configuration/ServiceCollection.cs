using System.Reflection;
using FogTalk.Infrastructure.Exceptions;

namespace FogTalk.API.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        services.AddScoped<ExceptionHandlingMiddleware>();
        return services;
    }
}