using System.Reflection;

namespace FogTalk.API.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        return services;
    }
}