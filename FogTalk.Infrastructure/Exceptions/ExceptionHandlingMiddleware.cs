using System.Security.Authentication;
using FogTalk.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace FogTalk.Infrastructure.Exceptions;

public class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
    {
        _logger = logger;
    }
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            string errorMessage = e switch
            {
                InvalidCredentialsException invalidCredentials => $"[{invalidCredentials.statusCode}] {invalidCredentials.GetType().Name} - {invalidCredentials.Message}",
                TokenAlreadyBlacklistedException tokenAlreadyBlacklisted => $"[{tokenAlreadyBlacklisted.statusCode}] {tokenAlreadyBlacklisted.GetType().Name} - {tokenAlreadyBlacklisted.Message}",
                UsernameTakenException usernameTaken => $"[{usernameTaken.statusCode}] {usernameTaken.GetType().Name} - {usernameTaken.Message}",
                EmailTakenException emailTaken => $"[{emailTaken.statusCode}] {emailTaken.GetType().Name} - {emailTaken.Message}",
                BadHttpRequestException badHttpRequest => $"[{badHttpRequest.StatusCode}] {badHttpRequest.GetType().Name} - {badHttpRequest.Message}",
                IdempotencyException idempotencyException => $"[{idempotencyException.statusCode}] {idempotencyException.GetType().Name} - {idempotencyException.Message}",
                _ => "[Middleware] An unexpected error occurred."
            };

            await context.Response.WriteAsync(errorMessage);
        }
    }
}