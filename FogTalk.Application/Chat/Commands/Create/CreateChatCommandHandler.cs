using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Mappings;
using FogTalk.Domain.Repositories;
using FogTalk.Domain.Shared;
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

    public async Task<Result<int>> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.Chat chat = request.ChatDto.Adapt<Domain.Entities.Chat>();
        await _repository.AddAsync(chat);
        request.ChatDto.Name = request.ChatDto.Name.Trim();
        return Result<int>.Success(chat.Id);
    }
}