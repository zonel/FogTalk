using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Application.Chat.Commands.Create;
using FogTalk.Domain.Repositories;
using FogTalk.Domain.Shared;
using Mapster;

namespace FogTalk.Application.User.Commands.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    #region ctor and props

    private readonly IGenericRepository<Domain.Entities.User, int> _repository;
    public RegisterUserCommandHandler(IGenericRepository<Domain.Entities.User, int> repository)
    {
        _repository = repository;
    }
    #endregion

    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        Domain.Entities.User user = request.registerUserDto.Adapt<Domain.Entities.User>();
        user.CreatedAt = DateTime.Now;
        await _repository.AddAsync(user);
    }
}