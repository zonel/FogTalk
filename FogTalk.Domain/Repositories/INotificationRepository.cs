namespace FogTalk.Domain.Repositories;

public interface INotificationRepository
{
    public Task<IEnumerable<T>> GetNotificationsAsync<T>(int userId, CancellationToken cancellationToken);
    public Task MarkNotificationAsReadAsync(int userId, int notificationId, CancellationToken cancellationToken);
    public Task DeleteNotificationAsync(int userId, int notificationId);
}