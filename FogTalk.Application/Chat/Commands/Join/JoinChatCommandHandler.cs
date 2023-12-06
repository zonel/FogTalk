using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Exceptions;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Chat.Commands.Join;

public class JoinChatCommandHandler : ICommandHandler<JoinChatCommand>
{
    private readonly IChatRepository _chatRepository;

    public JoinChatCommandHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    
    public async Task Handle(JoinChatCommand request, CancellationToken cancellationToken)
    {
        //idempotency check
        if (await _chatRepository.GetChatForUserByIdAsync(request.chatId, request.userId, cancellationToken) != null)
            throw new IdempotencyException("User is already in chat");
        
        await _chatRepository.AddUserToChatAsync(request.userId, request.chatId, cancellationToken);
    }
}