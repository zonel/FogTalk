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
        cancellationToken = request.Token;
        var user = await _userRepository.GetByEmailAsync(request.loginUserDto.Email, cancellationToken);
        if (user == null) throw new InvalidCredentialsException("Invalid email or password");
        if (!_passwordManager.ValidateAsync(request.loginUserDto.Password, user.Password, cancellationToken)) throw new InvalidCredentialsException("Invalid username or password");
        
        var jwt = _authenticator.CreateTokenAsync(user.Id, cancellationToken);
        return jwt;
    }
}