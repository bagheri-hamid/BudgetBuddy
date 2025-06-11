using BudgetBuddy.Application.Interfaces;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Serilog.Context;

namespace BudgetBuddy.Api.Middlewares;

public class UserIdLogContextMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context, ITokenHelper tokenHelper)
    {
        Guid? userId = null;

        try
        {
            userId = tokenHelper.GetUserId();
        }
        catch (UnauthorizedAccessException)
        {
            // user is not authenticated
        }

        if (userId.HasValue)
        {
            using (LogContext.PushProperty("UserId", userId.Value))
            {
                await next(context);
            }
        }
        else
        {
            await next(context);
        }
    }
}

public static class UserIdLogContextMiddlewareExtensions
{
    public static IApplicationBuilder UseUserIdLogContext(this IApplicationBuilder builder) =>
        builder.UseMiddleware<UserIdLogContextMiddleware>();
}