using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Friend.Commands.Create;

public class SendFriendRequestCommandHandler : ICommandHandler<SendFriendRequestCommand>
{
    private readonly IFriendRepository _friendRepository;

    public SendFriendRequestCommandHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }
    public async Task Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
    {
        await _friendRepository.SendFriendRequestAsync(request.currentUserId, request.receivingUserId);
    }
}