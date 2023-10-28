using System.Reflection;
using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Domain.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FogTalk.Application.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<CreateChatCommand, Result<int>>, CreateChatCommandHandler>(); // Register the CreateChatCommand handler
        return services;
    }
}