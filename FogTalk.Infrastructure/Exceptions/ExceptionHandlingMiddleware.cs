using System.Security.Authentication;
using FogTalk.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace FogTalk.Infrastructure.Exceptions;

public class ExceptionHandlingMiddleware : IMiddleware
{
    // public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    // {
    //     try
    //     {
    //         await next(context);
    //     }
    //     catch (InvalidCredentialsException e)
    //     {
    //         context.Response.StatusCode = 400;
    //         await context.Response.WriteAsync("[400] Invalid credentials - " + e.Message);
    //     }
    //     catch (TokenAlreadyBlacklistedException e)
    //     {
    //         context.Response.StatusCode = 400;
    //         await context.Response.WriteAsync("[400] TokenAlreadyBlacklistedException - " + e.Message);
    //     }
    //     catch (UsernameTakenException e)
    //     {
    //         context.Response.StatusCode = 400;
    //         await context.Response.WriteAsync("[400] UsernameTakenException - ");
    //     }
    //     catch (EmailTakenException e)
    //     {
    //         context.Response.StatusCode = 400;
    //         await context.Response.WriteAsync("[400] EmailTakenException - ");
    //     }
    //     catch (BadHttpRequestException e)
    //     {
    //         //context.Response.StatusCode = 400;
    //         await context.Response.WriteAsync("Exception occured - " + e.Message);
    //     }
    // }
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
                _ => "An unexpected error occurred."
            };

            await context.Response.WriteAsync(errorMessage);
        }
    }

}