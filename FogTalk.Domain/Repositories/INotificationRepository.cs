namespace FogTalk.Domain.Repositories;

public interface INotificationRepository
{
    public Task<IEnumerable<T>> GetNotificationsAsync<T>(int userId);
    public Task MarkNotificationAsReadAsync(int userId, int notificationId);
    public Task DeleteNotificationAsync(int userId, int notificationId);
}