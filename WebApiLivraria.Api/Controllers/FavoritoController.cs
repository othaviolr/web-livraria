using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.UseCases.Favorito;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiLivraria.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class FavoritoController : ControllerBase
    {
        private readonly IAdicionarFavoritoUseCase _adicionarUseCase;
        private readonly IRemoverFavoritoUseCase _removerUseCase;
        private readonly IListarFavoritosUseCase _listarUseCase;

        public FavoritoController(
            IAdicionarFavoritoUseCase adicionarUseCase,
            IRemoverFavoritoUseCase removerUseCase,
            IListarFavoritosUseCase listarUseCase)
        {
            _adicionarUseCase = adicionarUseCase;
            _removerUseCase = removerUseCase;
            _listarUseCase = listarUseCase;
        }

        private string ObterUsuarioId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            return userIdClaim;
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
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Ocorreu um erro inesperado no servidor.",
                    detail = ex.Message,
                    stackTrace = ex.StackTrace
                });
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
                bool existe = await _listarUseCase.VerificarFavorito(usuarioId, livroId.ToString());
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
}
