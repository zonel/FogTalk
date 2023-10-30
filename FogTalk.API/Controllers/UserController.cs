using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Chat.Dto;
using FogTalk.Application.Security.Dto;
using FogTalk.Application.User.Commands.Authenticate;
using FogTalk.Application.User.Commands.Register;
using FogTalk.Application.User.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FogTalk.API.Controllers;

public class UserController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] RegisterUserDto registerUserDto )
    {
        await _mediator.Send(new RegisterUserCommand(registerUserDto));
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<JwtDto> Login([FromBody] LoginUserDto loginUserDto)
    {
        return await _mediator.Send(new AuthenticateUserCommand(loginUserDto));
    }
}