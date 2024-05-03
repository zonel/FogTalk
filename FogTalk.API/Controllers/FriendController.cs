﻿using FogTalk.Application.Friend.Commands.Create;
using FogTalk.Application.Friend.Commands.Delete;
using FogTalk.Application.Friend.Commands.Update;
using FogTalk.Application.Friend.Dto;
using FogTalk.Application.Friend.Queries.Get;
using FogTalk.Application.Friend.Queries.GetFriendRequests;
using FogTalk.Infrastructure.SignalR;
using MediatR;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FogTalk.API.Controllers;

/// <summary>
/// 
/// </summary>
[ApiController]
[Microsoft.AspNetCore.Authorization.Authorize(Policy = "JtiPolicy")]
[Route("api/[controller]")]
public class FriendController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IHubContext<UserHub> _userHubContext;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="mediator"></param>
    /// <param name="userHubContext"></param>
    public FriendController(IMediator mediator, IHubContext<UserHub> userHubContext)
    {
        _mediator = mediator;
        _userHubContext = userHubContext;
    }
    
    /// <summary>
    /// Gets all friends of the user.
    /// </summary>
    /// <returns>
    /// List of friends.
    /// </returns>
    [HttpGet]
    public async Task<IEnumerable<ShowFriendDto>> Get(CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid)!.Value;
        var friends = await _mediator.Send(new GetUserFriendsQuery(Convert.ToInt32(userId), cancellationToken));
        return friends;
    }
    
    /// <summary>
    /// Gets all friend requests of the user.
    /// </summary>
    /// <returns>
    /// List of friend requests.
    /// </returns>
    [HttpGet("requests")]
    public async Task<IEnumerable<ShowFriendRequestDto>> GetFriendRequests(CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid)!.Value;
        var friends = await _mediator.Send(new GetUserFriendRequestsQuery(Convert.ToInt32(userId), cancellationToken));
        return friends;
    }


    /// <summary>
    /// Sends a friend request to the user with the given id.
    /// </summary>
    /// <param name="receivingUserId"> Id of the user that will receive friend request.</param>
    /// <param name="cancellationToken"></param>
    [HttpGet("{receivingUserId}")]
    public async Task<IActionResult> SendFriendRequest([FromRoute] int receivingUserId, CancellationToken cancellationToken)
    {
        var currentUserId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid)!.Value;
        await _mediator.Send(new SendFriendRequestCommand(Convert.ToInt32(currentUserId), receivingUserId, cancellationToken));
        await _userHubContext.Clients!.User(receivingUserId.ToString())!.SendFriendRequestNotification(receivingUserId, cancellationToken);
        return Ok();
    }

    /// <summary>
    /// Accept or decline friend request.
    /// </summary>
    /// <param name="requestingUserId"> Id of the user that sent a friend request.</param>
    /// <param name="accepted">accepted/declined</param>
    /// <param name="cancellationToken"></param>
    [HttpPut("{requestingUserId}")]
    public async Task<IActionResult> RespondToFriendRequest([FromRoute] int requestingUserId, [FromBody] bool accepted, CancellationToken cancellationToken)
    {
        var currentUserId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid)!.Value;
        await _mediator.Send(new RespondToFriendRequestCommand(Convert.ToInt32(currentUserId), requestingUserId, accepted, cancellationToken));
        return Ok();
    }

    /// <summary>
    /// Deletes a friend.
    /// </summary>
    /// <param name="userToRemoveId"> Id of the user to remove.</param>
    /// <param name="cancellationToken"></param>
    [HttpDelete]
    public async Task<IActionResult> RemoveFriend([FromBody] int userToRemoveId, CancellationToken cancellationToken)
    {
        var currentUserId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid)!.Value;
        await _mediator.Send(new RemoveFriendCommand(Convert.ToInt32(currentUserId), userToRemoveId, cancellationToken));
        return Ok();
    }

}