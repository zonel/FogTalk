using FogTalk.Application.Friend.Commands.Create;
using FogTalk.Application.Friend.Commands.Delete;
using FogTalk.Application.Friend.Commands.Update;
using FogTalk.Application.Friend.Dto;
using FogTalk.Application.Friend.Queries.Get;
using FogTalk.Application.Friend.Queries.GetFriendRequests;
using FogTalk.Application.User.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FogTalk.API.Controllers;

[ApiController]
[Authorize(Policy = "JtiPolicy")]
[Route("api/[controller]")]
public class FriendController : ControllerBase
{
    private readonly IMediator _mediator;

    public FriendController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ShowFriendDto>> Get()
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        var friends = await _mediator.Send(new GetUserFriendsQuery(Convert.ToInt32(userId)));
        return friends ?? new List<ShowFriendDto>();
    }
    
    [HttpGet("requests")]
    public async Task<IEnumerable<ShowFriendRequestDto>> GetFriendRequests()
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        var friends = await _mediator.Send(new GetUserFriendRequestsQuery(Convert.ToInt32(userId)));
        return friends;
    }
        
    [HttpGet("{receivingUserId}")]
    public async Task<IActionResult> SendFriendRequest([FromRoute] int receivingUserId)
    {
        var currentUserId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new SendFriendRequestCommand(Convert.ToInt32(currentUserId), receivingUserId));
        return Ok();
    }
    
    [HttpPut("{requestingUserId}")]
    public async Task<IActionResult> RespondToFriendRequest([FromRoute] int requestingUserId, [FromBody] bool Accepted)
    {
        var currentUserId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new RespondToFriendRequestCommand(Convert.ToInt32(currentUserId), requestingUserId, Accepted));
        return Ok();
    }
    
    [HttpDelete]
    public async Task<IActionResult> RemoveFriend([FromBody] int userToRemoveId)
    {
        var currentUserId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new RemoveFriendCommand(Convert.ToInt32(currentUserId), userToRemoveId));
        return Ok();
    }

}