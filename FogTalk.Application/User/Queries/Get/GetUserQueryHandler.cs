using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.User.Dto;
using FogTalk.Domain.Repositories;
using Mapster;

namespace FogTalk.Application.User.Queries.Get;

public class GetUserQueryHandler : IQueryHandler<GetUserQuery, ShowUserDto>
{
    private readonly IUserRepository _userRepository;
    private readonly IJtiRepository _jtiRepository;

    public GetUserQueryHandler(IUserRepository userRepository, IJtiRepository jtiRepository)
    {
        _userRepository = userRepository;
        _jtiRepository = jtiRepository;
    }
    
    public async Task<ShowUserDto> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        //add check if JWT token is valid 
        var user = await _userRepository.GetByIdAsync(request.userId, cancellationToken);
        var userDto = user.Adapt<ShowUserDto>();
        return userDto;
    }
}