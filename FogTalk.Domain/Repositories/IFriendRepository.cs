namespace FogTalk.Domain.Repositories;

public interface IFriendRepository
{
    public Task<IEnumerable<T>> GetUserFriendsAsync<T>(int userId, CancellationToken cancellationToken);
    public Task<IEnumerable<T>> GetUsersFriendRequestsAsync<T>(int userId, CancellationToken cancellationToken);
    public Task SendFriendRequestAsync(int userId, int friendId);
    public Task HandleFriendRequestAsync(int requestedUserId, int requestingUserId, bool isAccepted, CancellationToken cancellationToken);
    public Task RemoveFriendAsync(int userId, int userToDeleteId, CancellationToken cancellationToken);
}