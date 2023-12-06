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

    public async Task<IEnumerable<Chat>> GetChatsForUserAsync(int userId, CancellationToken token)
    {
        return await _dbContext.Chats.Where(c => c.Participants.Any(user => user.Id == userId)).ToListAsync(token);
    }

    public async Task<Chat> GetChatForUserByIdAsync(int chatId, int userId, CancellationToken token)
    {
        var chat = await _dbContext.Chats
            .Include(chat => chat.Participants)
            .FirstOrDefaultAsync(chat => chat.Id == chatId && chat.Participants.Any(user => user.Id == userId), token);

        return chat ?? new Chat();
    }
    
    public async Task AddUserToChatAsync(int userId, int chatId, CancellationToken token)
    {
        var chat = await _dbContext.Chats.Include(c => c.Participants).FirstOrDefaultAsync(c => c.Id == chatId, token);
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, token);
        
        if(chat == null || user == null)
            throw new InvalidOperationException("User or chat not found");
        
        chat.Participants.Add(user);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task RemoveUserFromChatAsync(int userId, int chatId, CancellationToken token)
    {
        var chat = await _dbContext.Chats.Include(c => c.Participants).FirstOrDefaultAsync(c => c.Id == chatId, token);
        var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.Id == userId, token);
        
        if(chat == null || user == null)
            throw new InvalidOperationException("User or chat not found");
        
        chat.Participants.Remove(user);
        await _dbContext.SaveChangesAsync(token);
    }
    
    public async Task UpdateAsync(Chat chat, CancellationToken token)
    {
        _dbContext.Chats.Update(chat);
        await _dbContext.SaveChangesAsync(token);
    }

    public async Task DeleteByIdAsync(int id, CancellationToken cancellationToken)
    {
        var chatToRemove =  await _dbContext.Chats.FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
        
        if(chatToRemove == null)
            throw new InvalidOperationException("Chat not found");
        
         _dbContext.Chats.Remove(chatToRemove);
         await _dbContext.SaveChangesAsync(cancellationToken);
    }
}