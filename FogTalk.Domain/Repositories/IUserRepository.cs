using System.Linq.Expressions;
using FogTalk.Domain.Entities;
using FogTalk.Domain.ValueObjects;

namespace FogTalk.Domain.Repositories;

public interface IUserRepository
{
    public Task<User> GetByIdAsync(int id, Func<IQueryable<User>, IQueryable<User>> include = null);   
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByUsernameAsync(string username);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
    public Task<bool> UserExistsAsync(Expression<Func<User, bool>> predicate);
    public Task<bool> UserHasAccessToChatAsync(int userId, int chatId);
    public Task<bool> UserHasAccessToMessageAsync(int userId, int chatId);

}