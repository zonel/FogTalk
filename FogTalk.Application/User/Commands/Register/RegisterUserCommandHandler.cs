using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Security;
using FogTalk.Domain.Exceptions;
using FogTalk.Domain.Repositories;
using Mapster;

namespace FogTalk.Application.User.Commands.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    #region ctor and props

    private readonly IGenericRepository<Domain.Entities.User, int> _repository;
    private readonly IPasswordManager _passwordManager;
    private readonly IUserRepository _userRepository;

    public RegisterUserCommandHandler(IGenericRepository<Domain.Entities.User, int> repository, IPasswordManager passwordManager, IUserRepository userRepository)
    {
        _passwordManager = passwordManager;
        _repository = repository;
        _userRepository = userRepository;
    }
    #endregion

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        if (await _userRepository.UserExistsAsync(u => u.UserName == request.UserDto.UserName))
            throw new UsernameTakenException("Username is already taken");
        if (await _userRepository.UserExistsAsync(u => u.Email == request.UserDto.Email))
            throw new EmailTakenException("Email is already taken");
        
        
        Domain.Entities.User user = request.UserDto.Adapt<Domain.Entities.User>();
        user.CreatedAt = DateTime.Now;
        user.Password =  _passwordManager.Secure(user.Password);
        await _repository.AddAsync(user);
    }
}