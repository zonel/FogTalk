namespace FogTalk.Domain.Repositories;

public interface IFriendRepository
{
    public Task<IEnumerable<T>> GetUsersFriends<T>(int userId);
    public IEnumerable<T> GetUsersFriendRequests<T>(int userId);
    public Task SendFriendRequestAsync(int userId, int friendId);
    public Task HandleFriendRequestAsync(int requestedUserId, int requestingUserId, bool isAccepted);
    public Task RemoveFriendAsync(int userId, int userToDeleteId);
}