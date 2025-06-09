using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.UseCases.Favorito;

namespace WebApiLivraria.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class FavoritoController : ControllerBase
{
    private readonly IAdicionarFavoritoUseCase _adicionarUseCase;
    private readonly RemoverFavoritoUseCase _removerUseCase;
    private readonly ListarFavoritosUseCase _listarUseCase;

    public FavoritoController(
        IAdicionarFavoritoUseCase adicionarUseCase,
        RemoverFavoritoUseCase removerUseCase,
        ListarFavoritosUseCase listarUseCase)
    {
        _adicionarUseCase = adicionarUseCase;
        _removerUseCase = removerUseCase;
        _listarUseCase = listarUseCase;
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarFavoritoRequest request)
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
    public async Task<ActionResult<List<FavoritoResponse>>> Listar(Guid usuarioId)
    {
        var favoritos = await _listarUseCase.Executar(usuarioId);
        return Ok(favoritos);
    }
}