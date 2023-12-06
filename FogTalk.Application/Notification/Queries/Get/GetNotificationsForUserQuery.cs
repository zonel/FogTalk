using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Notification.Dto;

namespace FogTalk.Application.Notification.Queries.Get;

public record GetNotificationsForUserQuery(int userId, CancellationToken CancellationToken) : IQuery<IEnumerable<NotificationDto>>;
