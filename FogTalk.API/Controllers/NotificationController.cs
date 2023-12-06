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
    
    /// <summary>
    /// Get all notifications for the current user
    /// </summary>
    /// <returns>
    /// List of notifications
    /// </returns>
    [HttpGet]
    public async Task<IEnumerable<NotificationDto>> Get(CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        var notifications = await _mediator.Send(new GetNotificationsForUserQuery(Convert.ToInt32(userId), cancellationToken));
        return notifications;
    }
    
    /// <summary>
    /// Mark a notification as read
    /// </summary>
    /// <param name="notificationId">Id of a notification</param>
    [HttpPost("{notificationId}")]
    public async Task MarkAsRead([FromRoute] int notificationId, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new MarkNotificationAsReadCommand(Convert.ToInt32(userId), notificationId, cancellationToken));
    }
    
}