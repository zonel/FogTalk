using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Notification.Commands.Delete;

public class DeleteNotificationCommandHandler : ICommandHandler<DeleteNotificationCommand>
{
    private readonly INotificationRepository _notificationRepository;

    public DeleteNotificationCommandHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }
    public async Task Handle(DeleteNotificationCommand request, CancellationToken cancellationToken)
    {
        await _notificationRepository.DeleteNotificationAsync(request.userId, request.notificationId);
    }
}
