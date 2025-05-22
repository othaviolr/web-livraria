using System.Net;
using System.Text.Json;
using WebApiLivraria.Domain.Exceptions;

namespace WebApiLivraria.Api.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro inesperado: {Mensagem}", ex.Message);
            await TratarErroAsync(context, ex);
        }
    }

    private static async Task TratarErroAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (statusCode, mensagem) = exception switch
        {
            EntidadeNaoEncontradaException => (HttpStatusCode.NotFound, exception.Message),
            RequisicaoInvalidaException => (HttpStatusCode.BadRequest, exception.Message),
            JaExisteException => (HttpStatusCode.Conflict, exception.Message),
            _ => (HttpStatusCode.InternalServerError, "Ocorreu um erro inesperado. Tente novamente mais tarde.")
        };

        context.Response.StatusCode = (int)statusCode;

        var resposta = RespostaPadrao<string>.ComErro(mensagem);

        await context.Response.WriteAsync(JsonSerializer.Serialize(resposta));
    }
}
