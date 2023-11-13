namespace FogTalk.Domain.Repositories;

public interface IFriendRepository
{
    public Task<IEnumerable<T>> GetUsersFriends<T>(int userId);
    public IEnumerable<T> GetUsersFriendRequests<T>(int userId);
    public Task SendFriendRequestAsync(int userId, int friendId); 
}