using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Dto;
using FogTalk.Domain.Repositories;
using Mapster;

namespace FogTalk.Application.Chat.Queries.GetById;

public class GetChatByIdQueryHandler : IQueryHandler<GetChatByIdQuery, ChatDto>
{
    private readonly IChatRepository _chatRepository;

    public GetChatByIdQueryHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    
    public async Task<ChatDto> Handle(GetChatByIdQuery request, CancellationToken cancellationToken)
    {
        var chat = await _chatRepository.GetChatForUserByIdAsync(request.chatId, request.userId);
        var chatDto = chat.Adapt<ChatDto>();
        return chatDto;
    }
}