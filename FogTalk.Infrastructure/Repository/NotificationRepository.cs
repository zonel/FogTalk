using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

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
        var friends = await _dbContext.Notifications
            .Where(u => u.Id == userId)
            .ToListAsync();

        if (friends.Count == 0)
        {
            return new List<T>();
        }
        
        var showUserDtos = friends.Adapt<List<T>>();
        return showUserDtos;
    }

    public async Task MarkNotificationAsReadAsync(int userId, int notificationId)
    {
        var notification = await _dbContext.Notifications
            .FirstOrDefaultAsync(u => u.Id == userId && u.Id == notificationId);

        if (notification == null)
        {
            return;
        }

        notification.IsRead = true;
        _dbContext.Notifications.Update(notification);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteNotificationAsync(int userId, int notificationId)
    {
        var notification = await _dbContext.Notifications.FirstOrDefaultAsync(u => u.Id == notificationId);
        
        if (notification == null)
        {
            return;
        }
        
        _dbContext.Notifications.Remove(notification);
        await _dbContext.SaveChangesAsync();
    }
}