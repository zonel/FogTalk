using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Chat.Commands.Delete;
using FogTalk.Application.Chat.Commands.Join;
using FogTalk.Application.Chat.Commands.Leave;
using FogTalk.Application.Chat.Commands.Update;
using FogTalk.Application.Chat.Dto;
using FogTalk.Application.Chat.Queries;
using FogTalk.Application.Chat.Queries.GetById;
using FogTalk.Application.User.Commands.Register;
using FogTalk.Domain.Exceptions;
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
    
    [HttpPost("join")]
    public async Task<IActionResult> Join([FromBody] int chatId)
    {
            var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
            await _mediator.Send(new JoinChatCommand(Convert.ToInt32(id), chatId));
            return Ok();
    }
    
    [HttpPost("leave")]
    public async Task<IActionResult> Leave([FromBody] int chatId)
    {
        try
        {
            var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
            await _mediator.Send(new LeaveChatCommand(Convert.ToInt32(id), chatId));
            return Ok();
        }
        catch (IdempotencyException e)
        {
            return Conflict(e.Message);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
    
    [HttpGet]
    public async Task<IEnumerable<ChatDto>> GetUserChatsQuery()
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        return await _mediator.Send(new GetUserChatsQuery(Convert.ToInt32(id)));
    }
    
    [HttpGet("{chatId}")]
    public async Task<ChatDto> GetChatDetails([FromRoute] int chatId)
    {
        var id = Convert.ToInt32(HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        return await _mediator.Send(new GetChatByIdQuery(id, chatId));
    }
    
    [HttpPut]
    public async Task UpdateChat([FromBody] UpdateChatDto UpdateChatDto)
    {
        var id = Convert.ToInt32(HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        await _mediator.Send(new UpdateChatCommand(id ,UpdateChatDto));
    }
    
    [HttpDelete]
    public async Task DeleteChat([FromBody] int chatId)
    {
        var id = Convert.ToInt32(HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        await _mediator.Send(new DeleteChatCommand(id, chatId));
    }
    
}