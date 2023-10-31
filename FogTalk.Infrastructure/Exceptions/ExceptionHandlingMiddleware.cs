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
        catch (InvalidCredentialsException e)
        {
            context.Response.StatusCode = 400;
            await context.Response.WriteAsync("[400] Invalid credentials - " + e.Message);
        }
    }
}