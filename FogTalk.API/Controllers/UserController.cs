using System.Security.Claims;
using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Chat.Dto;
using FogTalk.Application.Security.Dto;
using FogTalk.Application.User.Commands.Authenticate;
using FogTalk.Application.User.Commands.LogOut;
using FogTalk.Application.User.Commands.Register;
using FogTalk.Application.User.Dto;
using FogTalk.Application.User.Queries.Get;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FogTalk.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("register")]
    public async Task<ActionResult> Create([FromBody] UserDto userDto )
    {
        await _mediator.Send(new RegisterUserCommand(userDto));
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<JwtDto> Login([FromBody] LoginUserDto loginUserDto)
    {
        return await _mediator.Send(new AuthenticateUserCommand(loginUserDto));
    }
    
    [HttpPost("logout")]
    public async Task<ActionResult> LogOut([FromBody] JwtDto jwtDto)
    {
        await _mediator.Send(new LogOutUserCommand(jwtDto));
        return Ok();
    }
    
    [HttpGet]
    public async Task<ShowUserDto> GetCurrentUser()
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        return await _mediator.Send(new GetUserQuery(Convert.ToInt32(userId)));
    }
}