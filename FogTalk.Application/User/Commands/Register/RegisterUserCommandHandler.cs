using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Shared;

namespace FogTalk.Application.User.Commands.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
    }
}