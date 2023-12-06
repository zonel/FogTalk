using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Application.Chat.Commands.Delete;
using FogTalk.Application.Chat.Commands.Join;
using FogTalk.Application.Chat.Commands.Leave;
using FogTalk.Application.Chat.Commands.Update;
using FogTalk.Application.Chat.Dto;
using FogTalk.Application.Chat.Queries;
using FogTalk.Application.Chat.Queries.GetById;
using FogTalk.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.JsonWebTokens;

namespace FogTalk.API.Controllers;

/// <summary>
/// This controller is responsible for all chat related operations
/// </summary>
[ApiController]
[Authorize(Policy = "JtiPolicy")]
[Route("api/[controller]")]
public class ChatController : ControllerBase
{
    private readonly IMediator _mediator;
    
    /// <summary>
    /// Chat Controller constructor.
    /// </summary>
    /// <param name="mediator"></param>
    public ChatController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Creates a new chat.
    /// </summary>
    /// <remarks>
    /// Requires user to pass his JWT token in the header.
    /// </remarks>
    /// <param name="registerChatDto">Dto containing essential information about new chat.</param>
    [HttpPost("create")]
    public async Task<IActionResult> Create([FromBody] ChatDto registerChatDto, CancellationToken token)
    {
        await _mediator.Send(new CreateChatCommand(registerChatDto, token));
        return Ok();
    }
    
    /// <summary>
    /// Joins a user to a chat.
    /// </summary>
    /// <param name="chatId">Id of the chat to join.</param>
    [HttpPost("join")]
    public async Task<IActionResult> Join([FromBody] int chatId, CancellationToken token)
    {
            var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
            await _mediator.Send(new JoinChatCommand(Convert.ToInt32(id), chatId, token));
            return Ok();
    }
    
    /// <summary>
    /// Removes a user from a chat.
    /// </summary>
    /// <param name="chatId">Id of the chat to leave from.</param>
    /// <returns></returns>
    [HttpPost("leave")]
    public async Task<IActionResult> Leave([FromBody] int chatId, CancellationToken token)
    {
            var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
            await _mediator.Send(new LeaveChatCommand(Convert.ToInt32(id), chatId, token));
            return Ok();
    }
    
    /// <summary>
    /// Gets all chats that a user is a member of.
    /// </summary>
    /// <returns>
    /// Returns a list of all chats that a user is a member of.
    /// </returns>
    [HttpGet]
    public async Task<IEnumerable<ChatDto>> GetUserChatsQuery(CancellationToken token)
    {
        var id = HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value;
        return await _mediator.Send(new GetUserChatsQuery(Convert.ToInt32(id),token));
    }
    
    /// <summary>
    /// Gets details of a chat.
    /// </summary>
    /// <param name="chatId">Id of a chat</param>
    [HttpGet("{chatId}")]
    public async Task<ChatDto> GetChatDetails([FromRoute] int chatId, CancellationToken token)
    {
        var id = Convert.ToInt32(HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        return await _mediator.Send(new GetChatByIdQuery(id, chatId, token));
    }
    
    /// <summary>
    /// Updates a chat.
    /// </summary>
    /// <param name="UpdateChatDto">Dto with ale the parameters to update.</param>
    [HttpPut]
    public async Task<IActionResult> UpdateChat([FromBody] UpdateChatDto UpdateChatDto, CancellationToken token)
    {
        var id = Convert.ToInt32(HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        await _mediator.Send(new UpdateChatCommand(id ,UpdateChatDto, token));
        return Ok();
    }
    
    /// <summary>
    /// Deletes a chat.
    /// </summary>
    /// <param name="chatId">Id of a chat to delete.</param>
    [HttpDelete]
    public async Task<IActionResult> DeleteChat([FromBody] int chatId, CancellationToken token)
    {
        var id = Convert.ToInt32(HttpContext.User.FindFirst(JwtRegisteredClaimNames.Sid).Value);
        await _mediator.Send(new DeleteChatCommand(id, chatId, token));
        return Ok();
    }
    
}