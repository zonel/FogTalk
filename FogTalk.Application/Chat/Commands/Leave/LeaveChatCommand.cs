using FogTalk.Application.Abstraction.Messaging;

namespace FogTalk.Application.Chat.Commands.Leave;

public record LeaveChatCommand(int userId, int chatId, CancellationToken token) : ICommand;
