using FogTalk.Domain.Shared;
using MediatR;

namespace FogTalk.Application.Abstraction.Messaging;

//command without response
public interface ICommand : IRequest {}

//command with response
public interface ICommand<TResponse> : IRequest<TResponse> {}
