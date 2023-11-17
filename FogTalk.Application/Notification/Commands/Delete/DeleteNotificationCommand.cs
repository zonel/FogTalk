using FogTalk.Application.Abstraction.Messaging;

namespace FogTalk.Application.Notification.Commands.Delete;

public record DeleteNotificationCommand(int userId, int notificationId) : ICommand;
