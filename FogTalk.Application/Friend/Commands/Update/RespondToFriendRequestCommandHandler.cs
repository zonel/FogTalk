using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Friend.Commands.Update;

public class RespondToFriendRequestCommandHandler : ICommandHandler<RespondToFriendRequestCommand>
{
    private readonly IFriendRepository _friendRepository;

    public RespondToFriendRequestCommandHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }
    public async Task Handle(RespondToFriendRequestCommand request, CancellationToken cancellationToken)
    {
        await _friendRepository.HandleFriendRequestAsync(request.RequestedUserId, request.RequestingUserId, request.Accepted, cancellationToken);
    }
}