using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.User.Dto;

namespace FogTalk.Application.UserSearch.Commands;

public record SearchUserQuery(string searchPhrase, CancellationToken Token) : IQuery<IEnumerable<ShowUserDto>>;
