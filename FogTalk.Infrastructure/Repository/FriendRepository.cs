using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Mapster;

namespace FogTalk.Infrastructure.Repository;

public class FriendRepository : IFriendRepository
{
    private readonly FogTalkDbContext _dbContext;

    public FriendRepository(FogTalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<T>> GetUsersFriends<T>(int userId)
    {
        return _dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Friends)
            .ToList().Adapt<IEnumerable<T>>();
    }
    public IEnumerable<T> GetUsersFriendRequests<T>(int userId)
    {
        return _dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.FriendRequests)
            .ToList().Adapt<IEnumerable<T>>();
    }

    public async Task SendFriendRequestAsync(int userId, int friendId)
    {
        var receivingUser = await _dbContext.Users.FindAsync(friendId);
        
        _dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.FriendRequests)
            .ToList()
            .Add(receivingUser);
        
        await _dbContext.SaveChangesAsync();
    }
}