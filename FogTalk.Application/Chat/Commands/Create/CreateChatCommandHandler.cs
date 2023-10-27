using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Shared;

namespace FogTalk.Application.Chat.Commands.Create;

public class CreateChatCommandHandler : ICommandHandler<CreateChatCommand>
{
    public CreateChatCommandHandler()
    {

    }

    public Task<Result> Handle(CreateChatCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}