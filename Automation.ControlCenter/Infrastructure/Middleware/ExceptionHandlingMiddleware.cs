using Automation.ControlCenter.DTOs;
using Automation.ControlCenter.Infrastructure.Exceptions;
using Automation.ControlCenter.Services.Interfaces;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Text.Json;

namespace Automation.ControlCenter.Infrastructure.Middleware;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;
    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger; 
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ProcessNotFoundException ex)
        {
            _logger.LogError(
                ex,
                "Process not found");

            await WriteErrorAsync(
                context,
                HttpStatusCode.NotFound,
                "PROCESS_NOT_FOUND",
                ex.Message);
        }
        catch (InvalidStatusTransitionException ex)
        {

            _logger.LogError(
                ex,
                "Invalid status transition");

            await WriteErrorAsync(
                context,
                HttpStatusCode.BadRequest,
                "INVALID_STATUS_TRANSITION",
                ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(
                ex,
                "Unexpected error");

            await WriteErrorAsync(
                context,
                HttpStatusCode.InternalServerError,
                "UNEXPECTED_ERROR",
                "An unexpected error occurred");
        }
    }

    private static async Task WriteErrorAsync(
        HttpContext context,
        HttpStatusCode statusCode,
        string errorCode,
        string message)
    {
        var response = new ErrorResponse
        {
            ErrorCode = errorCode,
            Message = message
        };

        context.Response.StatusCode = (int)statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(response));
    }
}
