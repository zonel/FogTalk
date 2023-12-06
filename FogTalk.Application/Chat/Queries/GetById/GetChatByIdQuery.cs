using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;

namespace FogTalk.Application.Chat.Queries.GetById;

public record GetChatByIdQuery(int userId,int chatId, CancellationToken token) : IQuery<ChatDto>;
