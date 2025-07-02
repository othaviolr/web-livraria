using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.UseCases.Favorito;

namespace WebApiLivraria.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
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

    private Guid ObterUsuarioId()
    {
        var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (userIdClaim == null)
            throw new UnauthorizedAccessException("Usuário não autenticado.");

        return Guid.Parse(userIdClaim);
    }

    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] AdicionarFavoritoRequest request)
    {
        try
        {
            request.UsuarioId = ObterUsuarioId();
            await _adicionarUseCase.Executar(request);
            return Ok();
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { message = ex.Message });
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch
        {
            return StatusCode(500, new { message = "Ocorreu um erro inesperado no servidor." });
        }
    }

    [HttpDelete]
    public async Task<IActionResult> Remover([FromQuery] int livroId)
    {
        try
        {
            var usuarioId = ObterUsuarioId();
            await _removerUseCase.Executar(usuarioId, livroId);
            return NoContent();
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch
        {
            return StatusCode(500, new { message = "Ocorreu um erro inesperado no servidor." });
        }
    }

    [HttpGet]
    public async Task<ActionResult<List<FavoritoResponse>>> Listar()
    {
        try
        {
            var usuarioId = ObterUsuarioId();
            var favoritos = await _listarUseCase.Executar(usuarioId);
            return Ok(favoritos);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
    }

    [HttpGet("verificar/{livroId}")]
    public async Task<ActionResult<bool>> Verificar(int livroId)
    {
        try
        {
            var usuarioId = ObterUsuarioId();
            bool existe = await _listarUseCase.VerificarFavorito(usuarioId, livroId);
            return Ok(existe);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized();
        }
        catch
        {
            return StatusCode(500, new { message = "Ocorreu um erro inesperado no servidor." });
        }
    }
}