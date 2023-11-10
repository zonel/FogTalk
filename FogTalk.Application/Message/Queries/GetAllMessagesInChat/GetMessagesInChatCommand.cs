using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Message.Dto;

namespace FogTalk.Application.Message.Queries.GetMessagesInChat;

public record GetMessagesInChatCommand(int chatId,string cursor, int pageSize, CancellationToken cancellationToken) : IQuery<IEnumerable<ShowMessageDto>>;