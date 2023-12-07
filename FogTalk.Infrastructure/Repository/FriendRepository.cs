using FogTalk.Application.Friend.Dto;
using FogTalk.Domain.Exceptions;
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
    public async Task<IEnumerable<T>> GetUserFriendsAsync<T>(int userId, CancellationToken cancellationToken)
    {
        var friends = await _dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.Friends)
            .ToListAsync(cancellationToken);

        if (friends.Count == 0)
        {
            return new List<T>();
        }
        
        var showUserDtos = friends.Adapt<List<T>>();
        return showUserDtos;
    }
    public async Task<IEnumerable<T>> GetUsersFriendRequestsAsync<T>(int userId, CancellationToken cancellationToken)
    {
        var friendRequests = await _dbContext.Users
            .Where(u => u.Id == userId)
            .SelectMany(u => u.FriendRequests)
            .ToListAsync(cancellationToken);
        
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

    public async Task HandleFriendRequestAsync(int requestedUserId, int requestingUserId, bool isAccepted, CancellationToken cancellationToken)
    {
        var requestedUser = await _userRepository.GetByIdAsync(requestedUserId,cancellationToken, u => u.Include(u => u.Friends));
        var requestedUserWithFriendRequests = await _userRepository.GetByIdAsync(requestedUserId,cancellationToken, u => u.Include(u => u.FriendRequests));
        var requestingUser = await _userRepository.GetByIdAsync(requestingUserId,cancellationToken, u => u.Include(u => u.Friends));

        if (requestingUser == null || requestedUser == null)
            throw new InvalidOperationException("User not found");

        if (isAccepted)
        {
            requestedUser.Friends.Add(requestingUser);
            requestingUser.Friends.Add(requestedUser);

            await _userRepository.UpdateAsync(requestedUser, cancellationToken);
            await _userRepository.UpdateAsync(requestingUser, cancellationToken);
            
            //remove friend request
            requestedUserWithFriendRequests.FriendRequests.Remove(requestingUser);
            
            await _dbContext.SaveChangesAsync();
        }
        else
        {
            requestedUser.FriendRequests.Remove(requestingUser);
            await _userRepository.UpdateAsync(requestedUser, cancellationToken);
            await _dbContext.SaveChangesAsync();
        }

}

    public async Task RemoveFriendAsync(int userId, int userToDeleteId, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users.FindAsync(userId, cancellationToken);
        var userToDelete = await _dbContext.Users.FindAsync(userToDeleteId, cancellationToken);
        
        //idempotency check
        if(user.Friends.Any(f => f.Id == userToDeleteId) == false)
            throw new IdempotencyException("This user is not your friend.");
        
        if (user != null && userToDelete != null)
        {
            user.Friends.Remove(userToDelete);
            userToDelete.Friends.Remove(user);
            await _dbContext.SaveChangesAsync(cancellationToken);
            return;
        }
    }
}