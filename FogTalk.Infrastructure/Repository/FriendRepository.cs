using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Mapster;
using Microsoft.Identity.Client;

namespace FogTalk.Infrastructure.Repository;

public class FriendRepository : IFriendRepository
{
    private readonly FogTalkDbContext _dbContext;
    private readonly IUserRepository _userRepository;

    public FriendRepository(FogTalkDbContext dbContext, IUserRepository userRepository)
    {
        _dbContext = dbContext;
        _userRepository = userRepository;
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

    public async Task HandleFriendRequestAsync(int requestedUserId, int requestingUserId, bool isAccepted)
    {
        var requestedUser = await _userRepository.GetByIdAsync(requestedUserId);
        var requestingUser = await _userRepository.GetByIdAsync(requestingUserId);

        if (requestingUser == null || requestedUser == null)
            throw new InvalidOperationException("User not found");

        if (isAccepted)
        {
            requestedUser.Friends.Add(requestingUser);
            requestingUser.Friends.Add(requestedUser);

            await _userRepository.UpdateAsync(requestedUser);
            await _userRepository.UpdateAsync(requestingUser);
            
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            requestedUser.FriendRequests.Remove(requestingUser);
            await _userRepository.UpdateAsync(requestedUser);
            await _dbContext.SaveChangesAsync();
        }

}

    public Task RemoveFriendAsync(int userId, int userToDeleteId)
    {
        var user = _dbContext.Users.Find(userId);
        var userToDelete = _dbContext.Users.Find(userToDeleteId);
        
        user.Friends.Remove(userToDelete);
        userToDelete.Friends.Remove(user);
        
        return _dbContext.SaveChangesAsync();
    }
}