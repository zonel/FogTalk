using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Chat.Dto;
using FogTalk.Application.Chat.Queries;
using FogTalk.Application.User.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FogTalk.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    
    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ChatDto registerChatDto )
    {
        await _mediator.Send(new CreateChatCommand(registerChatDto));
        return Ok();
    }
    
    //make it based on JWT claim 
    [HttpGet]
    public async Task<IEnumerable<ChatDto>> GetUserChatsQuery()
    {
        return await _mediator.Send(new GetUserChatsQuery());
    }

    
}