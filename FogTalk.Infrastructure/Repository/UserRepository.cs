using System.Linq.Expressions;
using FogTalk.Domain.Entities;
using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace FogTalk.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly FogTalkDbContext _dbContext; 

    public UserRepository(FogTalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<User> GetByIdAsync(int id, CancellationToken cancellationToken, Func<IQueryable<User>, IQueryable<User>> include = null)
    {
        IQueryable<User> query = _dbContext.Users;

        if (include != null)
        {
            query = include(query);
        }

        var user = await query.FirstOrDefaultAsync(u => u.Id == id, cancellationToken);
        return user;
    }
    
    public async Task<bool> UserExistsAsync(Expression<Func<User, bool>> predicate, CancellationToken cancellationToken)
    {
        var userExists = await _dbContext.Users
            .AnyAsync(predicate, cancellationToken);
        return userExists;
    }

    public Task<bool> UserHasAccessToChatAsync(int userId, int chatId)
    {
        return _dbContext.Chats
            .AnyAsync(cu => cu.Id == chatId && cu.Participants.Any(u => u.Id == userId));
    }
    
    public Task<bool> UserHasAccessToMessageAsync(int userId, int messageId, CancellationToken cancellationToken)
    {
        return _dbContext.Messages
            .AnyAsync(m => m.Id == messageId && m.SenderId == userId, cancellationToken);
    }

    public async Task<User> GetByEmailAsync(string email, CancellationToken cancellationToken)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
        return user ?? new User();
    }

    public async Task<User> GetByUsernameAsync(string username)
    {
        var user = await _dbContext.Users
            .FirstOrDefaultAsync(u => u.UserName == username);
        return user ?? new User();
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.Users.AddAsync(user);
        await _dbContext.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _dbContext.Users.Update(user);
        await _dbContext.SaveChangesAsync();
    }
}