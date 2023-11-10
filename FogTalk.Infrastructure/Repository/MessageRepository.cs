using FogTalk.Domain.Entities;
using FogTalk.Domain.Repositories;
using FogTalk.Infrastructure.Persistence;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FogTalk.Infrastructure.Repository;

public class MessageRepository : IMessageRepository
{
    public MessageRepository(FogTalkDbContext context)
    {
        _DbContext = context;
    }

    public FogTalkDbContext _DbContext { get; set; }

    public async Task<List<ShowMessageDto>> GetMessagesAsync<ShowMessageDto>(int chatId, string cursor, int limit, CancellationToken cancellationToken)
    {
        IQueryable<Message> query = _DbContext.Messages
            .Where(m => m.ChatId == chatId)
            .OrderByDescending(m => m.Timestamp); // Order by timestamp descending

        if (!string.IsNullOrEmpty(cursor))
        {
            var cursorMessage = await _DbContext.Messages.FindAsync(Convert.ToInt32(cursor));

            if (cursorMessage != null)
            {
                query = query.Where(m => m.Timestamp < cursorMessage.Timestamp);
            }
        }

        query = query.Take(limit);
        var listQuery = query.ProjectToType<ShowMessageDto>();
        return await listQuery.ToListAsync();
    }
}