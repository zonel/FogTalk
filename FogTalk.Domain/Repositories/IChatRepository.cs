using FogTalk.Domain.Entities;

namespace FogTalk.Domain.Repositories;

public interface IChatRepository
{
    public Task<IEnumerable<Chat>> GetChatsForUserAsync(int userId);
    public Task<Chat> GetChatForUserByIdAsync(int chatId, int userId);
}