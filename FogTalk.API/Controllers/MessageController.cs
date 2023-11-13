using FogTalk.Application.Message.Commands.Create;
using FogTalk.Application.Message.Commands.Delete;
using FogTalk.Application.Message.Dto;
using FogTalk.Application.Message.Queries.GetMessagesInChat;
using FogTalk.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FogTalk.API.Controllers;

[ApiController]
[Authorize(Policy = "JtiPolicy")]
[Route("api/[controller]")]
public class MessageController : ControllerBase
{
    private readonly IMediator _mediator;
    public MessageController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    [HttpGet("{chatId}")]
    public async Task<IEnumerable<ShowMessageDto>> Get([FromRoute] int chatId, [FromQuery] string cursor = "", [FromQuery] int pageSize = 10)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        var messages = await _mediator.Send(new GetMessagesInChatCommand(chatId, cursor, pageSize, CancellationToken.None));
        return messages;
    }
    
    
    [HttpPost("{chatId}")]
    public async Task<IActionResult> Create([FromRoute] int chatId, [FromBody] MessageDto messageDto)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new CreateMessageCommand(messageDto, chatId, Convert.ToInt32(id)));
        return Ok();
    }

    [HttpDelete("{chatId}/{messageId}")]
    public async Task<IActionResult> Delete([FromRoute] int chatId,[FromRoute] int messageId)
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new DeleteMessageCommand(messageId, Convert.ToInt32(userId)));
        return Ok();
    }

}