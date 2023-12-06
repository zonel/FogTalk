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
    
    /// <summary>
    /// Gets messages in a chat. Cursor is used to get messages from a certain point in time for easy pagination.
    /// </summary>
    /// <param name="chatId">Id of a chat to get messages from.</param>
    /// <param name="cursor">An indicator utilized based on the timestamp of a message to determine the starting point for retrieving messages.</param>
    /// <param name="pageSize">How many messages to fetch per request.</param>
    /// <returns></returns>
    [HttpGet("{chatId}")]
    public async Task<IEnumerable<ShowMessageDto>> Get([FromRoute] int chatId, CancellationToken cancellationToken, [FromQuery] string cursor = "", [FromQuery] int pageSize = 10)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        var messages = await _mediator.Send(new GetMessagesInChatCommand(chatId, cursor, pageSize, cancellationToken));
        return messages;
    }
    
    /// <summary>
    /// Create a message in a chat.
    /// </summary>
    /// <param name="chatId">Id of a chat to send message in.</param>
    /// <param name="messageDto">Dto containing all essential informations to send message.</param>
    [HttpPost("{chatId}")]
    public async Task<IActionResult> Create([FromRoute] int chatId, [FromBody] MessageDto messageDto, CancellationToken cancellationToken)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new CreateMessageCommand(messageDto, chatId, Convert.ToInt32(id), cancellationToken));
        return Ok();
    }

    /// <summary>
    /// Deletes a message in a chat.
    /// </summary>
    /// <param name="chatId">Id of a chat to delete. </param>
    /// <param name="messageId">Id of a message to delete.</param>
    /// <returns></returns>
    [HttpDelete("{chatId}/{messageId}")]
    public async Task<IActionResult> Delete([FromRoute] int chatId,[FromRoute] int messageId, CancellationToken cancellationToken)
    {
        var userId = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        await _mediator.Send(new DeleteMessageCommand(messageId, Convert.ToInt32(userId), cancellationToken));
        return Ok();
    }

}