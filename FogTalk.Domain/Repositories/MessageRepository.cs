using FogTalk.Domain.Entities;

namespace FogTalk.Domain.Repositories;

public interface IMessageRepository
{
    public Task<List<T>> GetMessagesAsync<T>(int chatId, string cursor, int limit,
        CancellationToken cancellationToken);
}