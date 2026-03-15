using System.Net;
using System.Text.Json;
using Microsoft.Data.SqlClient;
using VKS.Credimatic.API.Models;

namespace VKS.Credimatic.API.Middleware;

public class ExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlerMiddleware> _logger;
    private readonly IHostEnvironment _env;

    public ExceptionHandlerMiddleware(RequestDelegate next, ILogger<ExceptionHandlerMiddleware> logger, IHostEnvironment env)
    {
        _next = next;
        _logger = logger;
        _env = env;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error no controlado: {Message}", ex.Message);
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, message, detail) = ex switch
        {
            SqlException sqlEx => (
                HttpStatusCode.ServiceUnavailable,
                "No se pudo conectar con la base de datos. Verifique la cadena de conexión, usuario y contraseña en appsettings.",
                _env.IsDevelopment() ? sqlEx.Message : null
            ),
            _ => (
                HttpStatusCode.InternalServerError,
                "Ocurrió un error interno. Intente de nuevo más tarde.",
                _env.IsDevelopment() ? ex.Message : null
            )
        };

        context.Response.StatusCode = (int)statusCode;

        var response = new ErrorResponse
        {
            Message = message,
            Detail = detail,
            StatusCode = (int)statusCode
        };

        var json = JsonSerializer.Serialize(response, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
        await context.Response.WriteAsync(json);
    }
}
