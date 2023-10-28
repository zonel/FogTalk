
using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;

namespace FogTalk.Application.Chat.Commands.Create;

public record CreateChatCommand(ChatDto ChatDto) : ICommand<int>
{
}