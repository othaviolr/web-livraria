using System.Net;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;

    public ExceptionMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (Exception ex)
        {
            await TratarExcecaoAsync(httpContext, ex);
        }
    }

    private static Task TratarExcecaoAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var respostaErro = new
        {
            sucesso = false,
            mensagem = "Ocorreu um erro interno no servidor.",
            detalhes = exception.Message // Opcional: remover em produção
        };

        var json = JsonSerializer.Serialize(respostaErro);

        return context.Response.WriteAsync(json);
    }
}
