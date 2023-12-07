using System.Security.AccessControl;
using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Chat.Commands.Delete;

public class DeleteChatCommandHandler : ICommandHandler<DeleteChatCommand>
{
    private readonly IChatRepository _chatRepository;

    public DeleteChatCommandHandler(IChatRepository chatRepository)
    {
        _chatRepository = chatRepository;
    }
    public async Task Handle(DeleteChatCommand request, CancellationToken cancellationToken)
    {
        cancellationToken = request.Token;
        if (_chatRepository.GetChatForUserByIdAsync(request.chatId, request.userId, cancellationToken) == null)
            throw new Exception("User is not a member of this chat.");
        
        await _chatRepository.DeleteByIdAsync(request.chatId, cancellationToken);
    }
}