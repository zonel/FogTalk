using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.User.Dto;

namespace FogTalk.Application.User.Commands.Register;

public sealed record RegisterUserCommand(UserDto UserDto, CancellationToken Token) : ICommand;