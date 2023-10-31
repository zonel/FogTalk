using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Security;
using FogTalk.Application.Security.Dto;
using FogTalk.Application.User.Dto;
using FogTalk.Domain.Exceptions;
using FogTalk.Domain.Repositories;

namespace FogTalk.Application.User.Commands.Authenticate;

public class AuthenticateUserCommandHandler : ICommandHandler<AuthenticateUserCommand,JwtDto>
{
    private readonly IAuthenticator _authenticator;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordManager _passwordManager;

    #region ctor and props

    public AuthenticateUserCommandHandler(IAuthenticator authenticator, IUserRepository userRepository, IPasswordManager passwordManager)
    {
        _authenticator = authenticator;
        _userRepository = userRepository;
        _passwordManager = passwordManager;
    }
    #endregion

    public async Task<JwtDto> Handle(AuthenticateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByEmailAsync(request.loginUserDto.Email);
        if (user == null) throw new InvalidCredentialsException("Invalid username or password");
        if (!_passwordManager.Validate(request.loginUserDto.Password, user.Password)) throw new InvalidCredentialsException("Invalid username or password");
        
        var jwt = _authenticator.CreateToken(user.Id);
        return jwt;
    }
}