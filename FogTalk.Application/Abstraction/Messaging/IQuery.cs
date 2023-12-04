using MediatR;

namespace FogTalk.Application.Abstraction.Messaging;

public interface IQuery<TResponse> : IRequest<TResponse>
{
}