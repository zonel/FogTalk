using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Friend.Dto;

namespace FogTalk.Application.Friend.Queries.GetFriendRequests;

public record GetUserFriendRequestsQuery(int userId, CancellationToken Token) : IQuery<IEnumerable<ShowFriendRequestDto>>;