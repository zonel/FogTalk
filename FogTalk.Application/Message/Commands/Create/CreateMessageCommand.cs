
using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Message.Dto;

namespace FogTalk.Application.Message.Commands.Create;

public record CreateMessageCommand(MessageDto messageDto, int chatId, int userId, CancellationToken CancellationToken) : ICommand;