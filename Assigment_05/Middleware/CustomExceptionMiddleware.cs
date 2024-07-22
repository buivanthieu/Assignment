using Microsoft.AspNetCore.Http;
using System;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Assigment_02.Error;
namespace Assigment_02.Middleware
{
    public class CustomExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<CustomExceptionMiddleware> _logger;

    public CustomExceptionMiddleware(RequestDelegate next, ILogger<CustomExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    public class NotFoundException : Exception
    {
        public NotFoundException(string message) : base(message)
        {
        }
    }

    public class BadRequestException : Exception
    {
        public BadRequestException(string message) : base(message)
        {
        }
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            _logger.LogError($"Something went wrong: {ex}");
            await HandleExceptionAsync(httpContext, ex);
        }
    }
    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        var statusCode = HttpStatusCode.InternalServerError;
        var errorMessage = "Internal Server Error from the custom middleware.";

        if (exception is NotFoundException)
        {
            statusCode = HttpStatusCode.NotFound;
            errorMessage = exception.Message;
        }
        else if (exception is BadRequestException)
        {
            statusCode = HttpStatusCode.BadRequest;
            errorMessage = exception.Message;
        }

        context.Response.StatusCode = (int)statusCode;

        var error = new ApiError
        {
            ErrorCode = context.Response.StatusCode,
            ErrorMessage = errorMessage
        };

        return context.Response.WriteAsync(JsonConvert.SerializeObject(error));
    }

}

}
