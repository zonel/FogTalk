using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Repositories;
using Mapster;

namespace FogTalk.Application.Chat.Commands.Create;

public class CreateChatCommandHandler : ICommandHandler<CreateChatCommand, int>
{
    #region ctor and props

    private readonly IGenericRepository<Domain.Entities.Chat, int> _repository;
    public CreateChatCommandHandler(IGenericRepository<Domain.Entities.Chat, int> repository)
    {
        _repository = repository;
    }
    #endregion

    public async Task<int> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        cancellationToken = request.cancellationToken;
        Domain.Entities.Chat chat = request.ChatDto.Adapt<Domain.Entities.Chat>();
        chat.Name = chat.Name.Trim();
        chat.CreatedAt = DateTime.Now;
        await _repository.AddAsync(chat, cancellationToken);
        return chat.Id;
    }
}