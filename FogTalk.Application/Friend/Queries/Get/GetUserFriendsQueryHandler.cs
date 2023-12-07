using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Friend.Dto;
using FogTalk.Application.User.Dto;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Friend.Queries.Get;

public class GetUserFriendsQueryHandler : IQueryHandler<GetUserFriendsQuery, IEnumerable<ShowFriendDto>>
{
    private readonly IFriendRepository _friendRepository;

    public GetUserFriendsQueryHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }
    public async Task<IEnumerable<ShowFriendDto>> Handle(GetUserFriendsQuery request, CancellationToken cancellationToken)
    {
        cancellationToken = request.Token;
        return await _friendRepository.GetUserFriendsAsync<ShowFriendDto>(request.userId, cancellationToken);
    }
}