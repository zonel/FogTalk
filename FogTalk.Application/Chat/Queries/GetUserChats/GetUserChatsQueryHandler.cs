using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;
using FogTalk.Domain.Repositories;
using FogTalk.Domain.Shared;
using Mapster;

namespace FogTalk.Application.Chat.Queries;

public class GetUserChatsQueryHandler : IQueryHandler<GetUserChatsQuery, IEnumerable<ChatDto>>
{
    #region ctor and props

    private readonly IGenericRepository<Domain.Entities.Chat, int> _repository;
    public GetUserChatsQueryHandler(IGenericRepository<Domain.Entities.Chat, int> repository)
    {
        _repository = repository;
    }
    #endregion
    
    public async Task<IEnumerable<ChatDto>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        var chats = await _repository.GetAllAsync();
        var chatdtos = chats.Adapt<IEnumerable<ChatDto>>();
        return chatdtos;
    }
}