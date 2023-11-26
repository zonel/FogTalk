using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace FogTalk.Infrastructure.SignalR;

public class NotificationHub : Hub
{
    private readonly IMediator _mediator;

    public NotificationHub(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task SendNotification(string message)
    {
        await Clients.All.SendAsync("ReceiveNotification", message);
    }
}