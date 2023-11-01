using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.User.Dto;
using FogTalk.Domain.Shared;

namespace FogTalk.Application.User.Commands.Register;

public sealed record RegisterUserCommand(UserDto UserDto) : ICommand;