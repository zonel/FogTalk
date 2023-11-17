using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Friend.Dto;
using FogTalk.Application.User.Dto;

namespace FogTalk.Application.Friend.Queries.Get;

public record GetUserFriendsQuery(int userId) : IQuery<IEnumerable<ShowFriendDto>>;