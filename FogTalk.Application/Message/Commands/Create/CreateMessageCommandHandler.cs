using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Repositories;
using Mapster;

namespace FogTalk.Application.Message.Commands.Create;

public class CreateMessageCommandHandler : ICommandHandler<CreateMessageCommand>
{
    private readonly IGenericRepository<Domain.Entities.Message, int> _repository;
    private readonly IUserRepository _userRepository;

    public CreateMessageCommandHandler(IGenericRepository<Domain.Entities.Message, int> repository, IUserRepository userRepository)
    {
        _repository = repository;
        _userRepository = userRepository;
    }
    public async Task Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        //check if user has access to chat
        if(!await _userRepository.UserHasAccessToChatAsync(request.userId, request.chatId))
            throw new UnauthorizedAccessException("You don't have access to this chat");
        
        Domain.Entities.Message message = request.messageDto.Adapt<Domain.Entities.Message>();
        message.ChatId = request.chatId;
        message.SenderId = request.userId;
        message.Timestamp = DateTime.Now;
        message.MessageStatus = Domain.Enums.MessageStatus.Sent;
        await _repository.AddAsync(message, cancellationToken);
    }
}