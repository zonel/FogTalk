using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Friend.Dto;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.Friend.Queries.GetFriendRequests;

public class GetUserFriendRequestsQueryHandler : IQueryHandler<GetUserFriendRequestsQuery, IEnumerable<ShowFriendRequestDto>>
{
    private readonly IFriendRepository _friendRepository;

    public GetUserFriendRequestsQueryHandler(IFriendRepository friendRepository)
    {
        _friendRepository = friendRepository;
    }
    
    public async Task<IEnumerable<ShowFriendRequestDto>> Handle(GetUserFriendRequestsQuery request, CancellationToken cancellationToken)
    {
        return await _friendRepository.GetUsersFriendRequestsAsync<ShowFriendRequestDto>(request.userId);
    }
}