using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;

namespace FogTalk.Application.Chat.Commands.Update;

public record UpdateChatCommand(int userId, UpdateChatDto chat) : ICommand;