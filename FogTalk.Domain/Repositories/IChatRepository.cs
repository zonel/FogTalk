using FogTalk.Domain.Entities;

namespace FogTalk.Domain.Repositories;

public interface IChatRepository
{
    public Task<IEnumerable<Chat>> GetChatsForUserAsync(int userId);
    public Task<Chat> GetChatForUserByIdAsync(int chatId, int userId);
    public Task AddUserToChatAsync(int userId, int chatId);
    public Task RemoveUserFromChatAsync(int userId, int chatId);
    public Task UpdateAsync(Chat chat);
    public Task DeleteByIdAsync(int id);
}