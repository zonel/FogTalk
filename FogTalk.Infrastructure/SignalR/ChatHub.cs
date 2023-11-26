using FogTalk.Application.Message.Commands.Create;
using FogTalk.Application.Message.Dto;
using MediatR;
using Microsoft.AspNet.SignalR;

namespace FogTalk.Infrastructure.SignalR;

public class ChatHub : Hub
{
    private readonly IMediator _mediator;

    public ChatHub(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    public async Task SendMessageToChat(int chatId, string message)
    {
        var userId = Context.User.Identity.Name;
        await _mediator.Send(new CreateMessageCommand(new MessageDto(message), chatId, Convert.ToInt32(userId)));
        await Clients.Group(chatId.ToString()).SendAsync("ReceiveMessage", message);
    }
}