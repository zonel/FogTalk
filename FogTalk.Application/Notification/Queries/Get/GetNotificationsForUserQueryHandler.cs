using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Notification.Dto;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Notification.Queries.Get;

public class GetNotificationsForUserQueryHandler : IQueryHandler<GetNotificationsForUserQuery, IEnumerable<NotificationDto>>
{
    private readonly INotificationRepository _notificationRepository;

    public GetNotificationsForUserQueryHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }
    public async Task<IEnumerable<NotificationDto>> Handle(GetNotificationsForUserQuery request, CancellationToken cancellationToken)
    {
        return await _notificationRepository.GetNotificationsAsync<NotificationDto>(request.userId, cancellationToken);
    }
}