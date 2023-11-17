using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Mapster;

namespace FogTalk.Infrastructure.Repository;

public class NotificationRepository : INotificationRepository
{
    private readonly FogTalkDbContext _dbContext;

    public NotificationRepository(FogTalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<T>> GetNotificationsAsync<T>(int userId)
    {
        var friends = _dbContext.Notifications
            .Where(u => u.Id == userId)
            .ToList();

        if (friends.Count == 0)
        {
            return new List<T>();
        }
        
        var showUserDtos = friends.Adapt<List<T>>();
        return showUserDtos;
    }

    public async Task MarkNotificationAsReadAsync(int userId, int notificationId)
    {
        var notification = _dbContext.Notifications
            .FirstOrDefault(u => u.Id == userId && u.Id == notificationId);

        if (notification == null)
        {
            return;
        }

        notification.IsRead = true;
        _dbContext.Notifications.Update(notification);
        await _dbContext.SaveChangesAsync();
    }

    public Task DeleteNotificationAsync(int userId, int notificationId)
    {
        var notification = _dbContext.Notifications.FirstOrDefault(u => u.Id == notificationId);
        
        if (notification == null)
        {
            return Task.CompletedTask;
        }
        
        _dbContext.Notifications.Remove(notification);
        return _dbContext.SaveChangesAsync();
    }
}