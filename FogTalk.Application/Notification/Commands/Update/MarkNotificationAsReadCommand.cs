using FogTalk.Application.Abstraction.Messaging;

namespace FogTalk.Application.Notification.Commands.Update;

public record MarkNotificationAsReadCommand(int userId, int notificationId, CancellationToken CancellationToken) : ICommand;