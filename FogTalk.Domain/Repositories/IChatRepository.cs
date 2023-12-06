using FogTalk.Domain.Entities;

namespace FogTalk.Domain.Repositories;

public interface IChatRepository
{
    public Task<IEnumerable<Chat>> GetChatsForUserAsync(int userId, CancellationToken token);
    public Task<Chat> GetChatForUserByIdAsync(int chatId, int userId, CancellationToken token);
    public Task AddUserToChatAsync(int userId, int chatId, CancellationToken token);
    public Task RemoveUserFromChatAsync(int userId, int chatId, CancellationToken token);
    public Task UpdateAsync(Chat chat, CancellationToken token);
    public Task DeleteByIdAsync(int id, CancellationToken token);
}