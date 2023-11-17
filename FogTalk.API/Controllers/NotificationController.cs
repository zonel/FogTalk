using FogTalk.Application.Friend.Dto;
using FogTalk.Application.Message.Dto;
using FogTalk.Application.Notification.Commands.Update;
using FogTalk.Application.Notification.Dto;
using FogTalk.Application.Notification.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FogTalk.API.Controllers;

[ApiController]
[Authorize(Policy = "JtiPolicy")]
[Route("api/[controller]")]
public class NotificationController : ControllerBase
{
    private readonly IMediator _mediator;

    public NotificationController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IEnumerable<NotificationDto>> Get()
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        var notifications = await _mediator.Send(new GetNotificationsForUserQuery(Convert.ToInt32(userId)));
        return notifications ?? new List<NotificationDto>();
    }
    
    [HttpPost("{notificationId}")]
    public async Task MarkAsRead([FromRoute] int notificationId)
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new MarkNotificationAsReadCommand(Convert.ToInt32(userId), notificationId));
    }
    
}