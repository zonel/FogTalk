using FogTalk.Domain.Entities;
using FogTalk.Domain.ValueObjects;

namespace FogTalk.Domain.Repositories;

public interface IUserRepository
{
    Task<User> GetByIdAsync(int id);
    Task<User> GetByEmailAsync(string email);
    Task<User> GetByUsernameAsync(string username);
    Task AddAsync(User user);
}