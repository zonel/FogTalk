using FogTalk.Application.Friend.Dto;
using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;
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
        var friends = _dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Friends)
            .ToList();

        if (friends.Count == 0)
        {
            return new List<T>();
        }
        
        var showUserDtos = friends.Adapt<List<T>>();
        return showUserDtos;
    }
    public IEnumerable<T> GetUsersFriendRequests<T>(int userId)
    {
        var friendRequests = _dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.FriendRequests)
            .ToList();
        
        if (friendRequests.Count() == 0)
        {
            return new List<T>();
        }
        
        return friendRequests.Adapt<IEnumerable<T>>();
    }

    public async Task SendFriendRequestAsync(int userId, int friendId)
    {
        var user = await _dbContext.Users.Include(c => c.FriendRequests).FirstOrDefaultAsync(c => c.Id == userId);
        var friend = await _dbContext.Users.FirstOrDefaultAsync(c => c.Id == friendId);
        
        if (user == null || friend == null)
            throw new InvalidOperationException("User not found");
        
        user.FriendRequests.Add(friend);
        await _dbContext.SaveChangesAsync();
    }

    public async Task HandleFriendRequestAsync(int requestedUserId, int requestingUserId, bool isAccepted)
    {
        var requestedUser = await _userRepository.GetByIdAsync(requestedUserId, u => u.Include(u => u.Friends));
        var requestedUserWithFriendRequests = await _userRepository.GetByIdAsync(requestedUserId, u => u.Include(u => u.FriendRequests));
        var requestingUser = await _userRepository.GetByIdAsync(requestingUserId, u => u.Include(u => u.Friends));

        if (requestingUser == null || requestedUser == null)
            throw new InvalidOperationException("User not found");

        if (isAccepted)
        {
            requestedUser.Friends.Add(requestingUser);
            requestingUser.Friends.Add(requestedUser);

            await _userRepository.UpdateAsync(requestedUser);
            await _userRepository.UpdateAsync(requestingUser);
            
            //remove friend request
            requestedUserWithFriendRequests.FriendRequests.Remove(requestingUser);
            
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