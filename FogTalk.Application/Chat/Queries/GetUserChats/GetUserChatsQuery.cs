using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;

namespace FogTalk.Application.Chat.Queries;

public record GetUserChatsQuery : IQuery<IEnumerable<ChatDto>>;
