using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Security.Dto;
using FogTalk.Application.User.Dto;

namespace FogTalk.Application.User.Queries.Get;

public record GetUserQuery(int userId, CancellationToken Token) : IQuery<ShowUserDto>
{
}