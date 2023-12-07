namespace FogTalk.Domain.Repositories;

public interface IUserSearchRepository
{
    public Task<IEnumerable<T>> SearchAsync<T>(string searchPhrase, CancellationToken token);
}