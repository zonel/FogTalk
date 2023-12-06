using System.Windows.Input;
using ICommand = FogTalk.Application.Abstraction.Messaging.ICommand;

namespace FogTalk.Application.Message.Commands.Delete;

public record DeleteMessageCommand(int messageId, int userId, CancellationToken CancellationToken) : ICommand;