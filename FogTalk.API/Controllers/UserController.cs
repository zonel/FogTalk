using System.Security.Claims;
using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Chat.Dto;
using FogTalk.Application.Security.Dto;
using FogTalk.Application.User.Commands.Authenticate;
using FogTalk.Application.User.Commands.LogOut;
using FogTalk.Application.User.Commands.Register;
using FogTalk.Application.User.Commands.Update;
using FogTalk.Application.User.Dto;
using FogTalk.Application.User.Queries.Get;
using FogTalk.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FogTalk.API.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<ActionResult> Create([FromBody] UserDto userDto )
    {
        if (!ModelState.IsValid)
        {
            var errors = ModelState.Values
                .SelectMany(v => v.Errors)
                .Select(e => e.ErrorMessage)
                .ToList();

            return BadRequest(new { errors });
        }
        
        await _mediator.Send(new RegisterUserCommand(userDto));
        return Ok();
    }
    
    [AllowAnonymous]
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
    
    [HttpGet("profile")]
    [Authorize]
    public async Task<ShowUserDto> GetCurrentUser()
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        return await _mediator.Send(new GetUserQuery(Convert.ToInt32(userId)));
    }
    
    [HttpPut("profile")]
    public async Task<ActionResult> UpdateCurrentUser([FromBody] UpdateUserDto userDto)
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new UpdateUserCommand(userDto, Convert.ToInt32(userId)));
        return Ok();
    }
}