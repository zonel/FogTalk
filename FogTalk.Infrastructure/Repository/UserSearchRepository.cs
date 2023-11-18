using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Mapster;

namespace FogTalk.Infrastructure.Repository;

public class UserSearchRepository : IUserSearchRepository
{
    private readonly FogTalkDbContext _dbContext;

    public UserSearchRepository(FogTalkDbContext dbContext)
    {
        _dbContext = dbContext;
    }
    public async Task<IEnumerable<T>> SearchAsync<T>(string searchPhrase)
    {
        var users = _dbContext.Users.Where(u => u.UserName.Contains(searchPhrase)).ToList();
        
        if (users.Count == 0)
        {
            return new List<T>();
        }
        
        var showUserDtos = users.Adapt<List<T>>();
        return showUserDtos;
    }

}