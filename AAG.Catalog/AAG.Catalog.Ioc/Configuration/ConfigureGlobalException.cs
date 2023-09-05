using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Data.SqlClient;

namespace AAG.Catalog.Ioc.Configuration;

public class ConfigureGlobalException : IMiddleware
{
    private readonly ILogger<ConfigureGlobalException> _logger;

    public ConfigureGlobalException(ILogger<ConfigureGlobalException> logger)
    {
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (SqlException ex)
        {
            _logger.LogError($"Erro inesperado: {ex}");
            await HandleExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {

            if (!(ex is Win32Exception w32ex))
                w32ex = ex.InnerException as Win32Exception;

            int statusCode = w32ex != null ? w32ex.ErrorCode : ex.HResult;

            _logger.LogError($"Erro inesperado: {ex}");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        const int statusCode = StatusCodes.Status500InternalServerError;

        var json = JsonConvert.SerializeObject(new
        {
            statusCode,
            message = "Ocorreu um erro no processamento da requisição",
            detailed = exception
        });

        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        return context.Response.WriteAsync(json);
    }
}
