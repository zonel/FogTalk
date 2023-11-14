using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Friend.Commands.Delete;

public class RemoveFriendCommandHandler : ICommandHandler<RemoveFriendCommand>
{
    private readonly IFriendRepository _friendRepository;

    public RemoveFriendCommandHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    } 
    
    public async Task Handle(RemoveFriendCommand request, CancellationToken cancellationToken)
    {
        await _friendRepository.RemoveFriendAsync(request.userId, request.userToDeleteId);
    }
}