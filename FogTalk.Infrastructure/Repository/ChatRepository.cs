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
        return await _dbContext.Chats.Where(c => c.Participants.Any(user => user.Id == userId)).ToListAsync();
    }

    public async Task<Chat> GetChatForUserByIdAsync(int chatId, int userId)
    {
        var chat = await _dbContext.Chats
            .Include(chat => chat.Participants)
            .FirstOrDefaultAsync(chat => chat.Id == chatId && chat.Participants.Any(user => user.Id == userId));

        return chat ?? new Chat();
    }
    
    public async Task AddUserToChatAsync(int userId, int chatId)
    {
        var chat = await _dbContext.Chats.Include(c => c.Participants).FirstOrDefaultAsync(c => c.Id == chatId);
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        if(chat == null || user == null)
            throw new InvalidOperationException("User or chat not found");
        
        chat.Participants.Add(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task RemoveUserFromChatAsync(int userId, int chatId)
    {
        var chat = await _dbContext.Chats.Include(c => c.Participants).FirstOrDefaultAsync(c => c.Id == chatId);
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId);
        
        if(chat == null || user == null)
            throw new InvalidOperationException("User or chat not found");
        
        chat.Participants.Remove(user);
        await _dbContext.SaveChangesAsync();
    }
    
    public async Task UpdateAsync(Chat chat)
    {
        _dbContext.Chats.Update(chat);
        await _dbContext.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync(int id)
    {
        var chatToRemove =  _dbContext.Chats.FirstOrDefault(c => c.Id == id);
        
        if(chatToRemove == null)
            throw new InvalidOperationException("Chat not found");
        
         _dbContext.Chats.Remove(chatToRemove);
         await _dbContext.SaveChangesAsync();
    }
}