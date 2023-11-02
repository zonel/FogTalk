using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.User.Dto;

namespace FogTalk.Application.User.Commands.Update;

public record UpdateUserCommand(UpdateUserDto userDto, int userId) : ICommand
{
}