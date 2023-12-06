using FogTalk.Application.Abstraction.Messaging;

namespace FogTalk.Application.Chat.Commands.Join;

public record JoinChatCommand(int userId, int chatId, CancellationToken token) : ICommand
{
}