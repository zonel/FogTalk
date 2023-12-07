using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.User.Dto;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.UserSearch.Commands;

public class SearchUserQueryHandler : IQueryHandler<SearchUserQuery, IEnumerable<ShowUserDto>>
{
    private readonly IUserSearchRepository _userSearchRepository;

    public SearchUserQueryHandler(IUserSearchRepository userSearchRepository)
    {
        _userSearchRepository = userSearchRepository;
    }
    public async Task<IEnumerable<ShowUserDto>> Handle(SearchUserQuery request, CancellationToken cancellationToken)
    {
        cancellationToken = request.Token;
        return await _userSearchRepository.SearchAsync<ShowUserDto>(request.searchPhrase, cancellationToken);
    }
}