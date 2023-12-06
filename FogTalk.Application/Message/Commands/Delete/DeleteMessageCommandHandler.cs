using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Message.Commands.Delete;

public class DeleteMessageCommandHandler : ICommandHandler<DeleteMessageCommand>
{
    private readonly IMessageRepository _messageRepository;
    private readonly IUserRepository _userRepository;

    public DeleteMessageCommandHandler(IMessageRepository messageRepository, IUserRepository userRepository)
    {
        _messageRepository = messageRepository;
        _userRepository = userRepository;
    }
    public async Task Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        //check if user has access to chat
        if(!await _userRepository.UserHasAccessToMessageAsync(request.userId, request.messageId, cancellationToken))
            throw new UnauthorizedAccessException("You don't have access to this message");
        
        await _messageRepository.RemoveMessagesAsync(request.messageId, cancellationToken);
    }
}