using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Friend.Commands.Delete;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Friend.Commands.RemoveFriend;

public class RemoveFriendCommandHandler : ICommandHandler<RemoveFriendCommand>
{
    private readonly IFriendRepository _friendRepository;

    public RemoveFriendCommandHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    } 
    
    public async Task Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
    {
        await _friendRepository.RemoveFriendAsync(request.userId, request.userToDeleteId, cancellationToken);
    }
}