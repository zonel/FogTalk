using FogTalk.Application.Abstraction.Messaging;
using FogTalk.Domain.Shared;

namespace FogTalk.Application.User.Commands.Register;

internal sealed class RegisterUserCommandHandler : ICommandHandler<RegisterUserCommand>
{
    public async Task<Result> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        return Result.Success();
    }
}