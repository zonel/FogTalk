using FogTalk.Application.Abstraction.Messaging;

namespace FogTalk.Application.Chat.Commands.Delete;

public record DeleteChatCommand(int userId, int chatId, CancellationToken Token) : ICommand;