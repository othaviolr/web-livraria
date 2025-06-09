using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Application.UseCases.ListaDesejo;

namespace WebApiLivraria.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ListaDesejoController : ControllerBase
{
    private readonly IAdicionarListaDesejoUseCase _adicionarUseCase;
    private readonly RemoverListaDesejoUseCase _removerUseCase;
    private readonly ListarListaDesejoUseCase _listarUseCase;

    public ListaDesejoController(
        IAdicionarListaDesejoUseCase adicionarUseCase,
        RemoverListaDesejoUseCase removerUseCase,
        ListarListaDesejoUseCase listarUseCase)
    {
        _adicionarUseCase = adicionarUseCase;
        _removerUseCase = removerUseCase;
        _listarUseCase = listarUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarListaDesejoRequest request)
    {
        await _adicionarUseCase.Executar(request);
        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Remover([FromQuery] Guid usuarioId, [FromQuery] Guid livroId)
    {
        await _removerUseCase.Executar(usuarioId, livroId);
        return NoContent();
    }

    [HttpGet("{usuarioId}")]
    public async Task<ActionResult<List<ListaDesejoResponse>>> Listar(Guid usuarioId)
    {
        var lista = await _listarUseCase.Executar(usuarioId);
        return Ok(lista);
    }
}