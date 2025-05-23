using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;

public class TratamentoExcecaoMiddleware
{
    private readonly RequestDelegate _next;

    public TratamentoExcecaoMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await TratarExcecaoAsync(context, ex);
        }
    }

    private Task TratarExcecaoAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var resposta = RespostaPadrao<string>.ComErro("Ocorreu um erro inesperado no servidor.");

        var json = JsonSerializer.Serialize(resposta);

        // Aqui pode adicionar log da exceção ex, se quiser

        return context.Response.WriteAsync(json);
    }
}
