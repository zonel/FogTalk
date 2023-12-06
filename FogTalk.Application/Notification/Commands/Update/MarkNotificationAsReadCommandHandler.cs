using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Notification.Commands.Update;

public class MarkNotificationAsReadCommandHandler : ICommandHandler<MarkNotificationAsReadCommand>
{
    private readonly INotificationRepository _notificationRepository;

    public MarkNotificationAsReadCommandHandler(INotificationRepository notificationRepository)
    {
        _notificationRepository = notificationRepository;
    }
    
    public async Task Handle(MarkNotificationAsReadCommand request, CancellationToken cancellationToken)
    {
        await _notificationRepository.MarkNotificationAsReadAsync(request.userId, request.notificationId, cancellationToken);
    }
}