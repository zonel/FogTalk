using System.Linq.Expressions;
using FogTalk.Domain.Entities;
using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

public class ChatRepository : IChatRepository
{
    private readonly FogTalkDbContext _dbContext; 

    public ChatRepository(FogTalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Chat>> GetChatsForUserAsync(int userId)
    {
        return await _dbContext.Chats.Where(c => c.Id == userId).ToListAsync();
    }

    public async Task<Chat> GetChatForUserByIdAsync(int chatId, int userId)
    {
        return await _dbContext.Chats
            .Include(chat => chat.Participants)
            .FirstOrDefaultAsync(chat => chat.Id == chatId && chat.Participants.Any(user => user.Id == userId));
    }
}