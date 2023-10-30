using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Chat.Dto;
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
    
    /// <summary>
    /// Creates a new user
    /// </summary>
    /// <param name="registerUserDto"></param>
    /// <returns></returns>
    [HttpPost("create")]
    public async Task<ActionResult> Create([FromBody] RegisterUserDto registerUserDto )
    {
        await _mediator.Send(new RegisterUserCommand(registerUserDto));
        return Ok();
    }
}