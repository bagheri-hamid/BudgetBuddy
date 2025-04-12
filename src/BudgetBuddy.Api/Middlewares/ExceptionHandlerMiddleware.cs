using System.Net;
using System.Text.Json;
using BudgetBuddy.Api.ViewModels;
using BudgetBuddy.Api.ViewModels.V1;
using BudgetBuddy.Domain.Exceptions;
using Microsoft.AspNetCore.Http;

namespace BudgetBuddy.Api.Middlewares;

public class ExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (DomainException e)
        {
            await HandleDomainException(context, e);
        }
        catch (Exception e)
        {
            await HandleUnhandledException(context, e);
        }
    }

    private static Task HandleDomainException(HttpContext context, DomainException exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = exception.StatusCode;

        var response = new Response<object?>(exception.StatusCode, exception.Message, false, null);
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static Task HandleUnhandledException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var response = new Response<object?>((int)HttpStatusCode.InternalServerError, exception.Message, false, null);
        return context.Response.WriteAsync(JsonSerializer.Serialize(response));
    }
}