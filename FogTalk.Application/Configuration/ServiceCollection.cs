using System.Reflection;
using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Security;
using FogTalk.Application.Security.Dto;
using FogTalk.Application.User.Commands.Authenticate;
using FogTalk.Application.User.Commands.LogOut;
using FogTalk.Application.User.Commands.Register;
using FogTalk.Application.User.Commands.Update;
using FogTalk.Application.User.Dto;
using FogTalk.Application.User.Queries.Get;
using FogTalk.Domain.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FogTalk.Application.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => { cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()); });
        
        return services;
    }
}