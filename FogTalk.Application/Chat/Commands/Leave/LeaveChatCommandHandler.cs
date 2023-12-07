using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Commands.Join;
using FogTalk.Domain.Exceptions;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Chat.Commands.Leave;

public class LeaveChatCommandHandler : ICommandHandler<LeaveChatCommand>
{
    private readonly IChatRepository _chatRepository;

    public LeaveChatCommandHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    
    public async Task Handle(LeaveChatCommand request, CancellationToken cancellationToken)
    {
        cancellationToken = request.token;
        //idempotency check
        if (await _chatRepository.GetChatForUserByIdAsync(request.chatId, request.userId, cancellationToken) == null)
            throw new IdempotencyException("User does not belong to this chat.");
        
        await _chatRepository.RemoveUserFromChatAsync(request.userId, request.chatId, cancellationToken);
    }

}