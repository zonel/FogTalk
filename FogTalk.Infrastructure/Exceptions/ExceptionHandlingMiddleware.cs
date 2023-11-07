using System.Security.Authentication;
using FogTalk.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FogTalk.Infrastructure.Exceptions;

public class ExceptionHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            context.Response.StatusCode = 400; // Set the appropriate status code

            string errorMessage = e switch
            {
                InvalidCredentialsException invalidCredentials => $"[400] {invalidCredentials.GetType().Name} - {invalidCredentials.Message}",
                TokenAlreadyBlacklistedException tokenAlreadyBlacklisted => $"[400] {tokenAlreadyBlacklisted.GetType().Name} - {tokenAlreadyBlacklisted.Message}",
                UsernameTakenException usernameTaken => $"[400] {usernameTaken.GetType().Name}",
                EmailTakenException emailTaken => $"[400] {emailTaken.GetType().Name}",
                BadHttpRequestException badHttpRequest => $"Exception occurred - {badHttpRequest.Message}",
                IdempotencyException idempotencyException => $"[400] {idempotencyException.GetType().Name} - {idempotencyException.Message}",
                _ => "[Middleware] An unexpected error occurred."
            };

            await context.Response.WriteAsync(errorMessage);
        }
    }
}