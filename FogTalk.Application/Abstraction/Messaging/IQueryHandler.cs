using FogTalk.Domain.Shared;
using MediatR;

namespace FogTalk.Application.Abstraction.Messaging;

public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>> 
    where TQuery : IQuery<TResponse>
{
}