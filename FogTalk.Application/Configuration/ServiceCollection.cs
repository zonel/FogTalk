using System.Reflection;
using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Security;
using FogTalk.Application.Security.Dto;
using FogTalk.Application.User.Commands.Authenticate;
using FogTalk.Application.User.Commands.Register;
using FogTalk.Domain.Shared;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace FogTalk.Application.Configuration;

public static class ServiceCollection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddTransient<IRequestHandler<CreateChatCommand, int>, CreateChatCommandHandler>(); // Register the CreateChatCommand handler
        services.AddTransient<IRequestHandler<RegisterUserCommand>, RegisterUserCommandHandler>(); // Register the CreateChatCommand handler
        services.AddTransient<IRequestHandler<AuthenticateUserCommand, JwtDto>, AuthenticateUserCommandHandler>();
        return services;
    }
}