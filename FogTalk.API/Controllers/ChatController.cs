using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Chat.Dto;
using FogTalk.Application.Chat.Queries;
using FogTalk.Application.Chat.Queries.GetById;
using FogTalk.Application.User.Commands.Register;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FogTalk.API.Controllers;

[ApiController]
[Authorize(Policy = "JtiPolicy")]
[Route("api/[controller]")]
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
    
    [HttpGet("/{chatId}")]
    public async Task<ChatDto> GetChatDetails([FromRoute] int chatId)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        return await _mediator.Send(new GetChatByIdQuery(Convert.ToInt32(id)));
    }


    
}