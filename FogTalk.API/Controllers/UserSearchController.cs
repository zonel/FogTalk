﻿using FogTalk.Application.User.Dto;
using FogTalk.Application.UserSearch.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FogTalk.API.Controllers;


[ApiController]
[Authorize(Policy = "JtiPolicy")]
[Route("api/search")]
public class UserSearchController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserSearchController(IMediator mediator)
    {
        _mediator = mediator;
    }
    
    /// <summary>
    /// Searches for users by his username.
    /// </summary>
    /// <param name="searchPhrase">Phrase that must be contained in user's username.</param>
    [HttpGet("user")]
    public async Task<IEnumerable<ShowUserDto>> SearchUsers([FromBody] string searchPhrase)
    {
        var searchResults = await _mediator.Send(new SearchUserQuery(searchPhrase));
        return searchResults ?? new List<ShowUserDto>();
    }
}