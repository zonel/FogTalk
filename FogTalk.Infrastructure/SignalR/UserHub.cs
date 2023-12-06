using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace FogTalk.Infrastructure.SignalR;

public class UserHub : Hub
{
    private readonly IMediator _mediator;

    public UserHub(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task SendFriendRequestNotification(int receivingUserId, CancellationToken cancellationToken)
    {
        await Clients.User(receivingUserId.ToString()).SendAsync("ReceiveFriendRequestNotification", cancellationToken);
    }
}