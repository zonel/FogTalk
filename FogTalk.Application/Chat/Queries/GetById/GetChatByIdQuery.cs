using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;

namespace FogTalk.Application.Chat.Queries.GetById;

public record GetChatByIdQuery(int chatId) : IQuery<ChatDto>;
