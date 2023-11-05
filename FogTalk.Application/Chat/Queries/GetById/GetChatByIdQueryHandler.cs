using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;
using FogTalk.Domain.Repositories;
using Mapster;

namespace FogTalk.Application.Chat.Queries.GetById;

public class GetChatByIdQueryHandler : IQueryHandler<GetChatByIdQuery, ChatDto>
{
    private readonly IGenericRepository<Domain.Entities.Chat, int> _chatRepository;

    public GetChatByIdQueryHandler(IGenericRepository<Domain.Entities.Chat, int> chatRepository)
    {
        _chatRepository = chatRepository;
    }
    
    public async Task<ChatDto> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetByIdAsync(request.chatId);
        var chatDto = chat.Adapt<ChatDto>();
        return chatDto;
    }
}