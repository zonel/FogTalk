using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;
using FogTalk.Domain.Repositories;
using Mapster;

namespace FogTalk.Application.Chat.Queries;

public class GetUserChatsQueryHandler : IQueryHandler<GetUserChatsQuery, IEnumerable<ChatDto>>
{
    #region ctor and props

    private readonly IGenericRepository<Domain.Entities.Chat, int> _repository;
    private readonly IChatRepository _chatRepository;

    public GetUserChatsQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    #endregion
    
    public async Task<IEnumerable<ChatDto>> Handle(GetUserChatsQuery request, CancellationToken cancellationToken)
    {
        cancellationToken = request.token;
        var chats = await _chatRepository.GetChatsForUserAsync(request.userId, cancellationToken);
        var chatdtos = chats.Adapt<IEnumerable<ChatDto>>();
        return chatdtos;
    }
}