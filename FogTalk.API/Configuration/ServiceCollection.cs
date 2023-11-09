using System.Reflection;
using FogTalk.Infrastructure.Exceptions;
using Serilog;

namespace FogTalk.API.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        #region Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
            .CreateLogger();
        
        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.AddSerilog();
        });
        #endregion
        
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        services.AddScoped<ExceptionHandlingMiddleware>();
        return services;
    }
}