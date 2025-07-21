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
            if (!ModelState.IsValid)
                return BadRequest(RespostaPadrao<string>.ComErro("Dados inválidos para adicionar favorito."));

            try
            {
                request.UsuarioId = ObterUsuarioId();
                await _adicionarUseCase.Executar(request);
                return Ok(RespostaPadrao<string>.ComSucesso("Livro adicionado aos favoritos com sucesso."));
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(RespostaPadrao<string>.ComErro(ex.Message));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(RespostaPadrao<string>.ComErro("Usuário não autorizado."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, RespostaPadrao<string>.ComErro($"Erro interno: {ex.Message}"));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Remover([FromQuery] string livroId)
        {
            if (string.IsNullOrWhiteSpace(livroId))
                return BadRequest(RespostaPadrao<string>.ComErro("LivroId não pode ser vazio."));

            try
            {
                var usuarioId = ObterUsuarioId();
                await _removerUseCase.Executar(usuarioId, livroId);
                return Ok(RespostaPadrao<string>.ComSucesso("Livro removido dos favoritos com sucesso."));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(RespostaPadrao<string>.ComErro("Usuário não autorizado."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, RespostaPadrao<string>.ComErro($"Erro interno: {ex.Message}"));
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var usuarioId = ObterUsuarioId();
                var favoritos = await _listarUseCase.Executar(usuarioId);
                return Ok(RespostaPadrao<List<FavoritoResponse>>.ComSucesso(favoritos, "Favoritos listados com sucesso."));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(RespostaPadrao<string>.ComErro("Usuário não autorizado."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, RespostaPadrao<string>.ComErro($"Erro interno: {ex.Message}"));
            }
        }

        [HttpGet("verificar/{livroId}")]
        public async Task<IActionResult> Verificar(string livroId)
        {
            if (string.IsNullOrWhiteSpace(livroId))
                return BadRequest(RespostaPadrao<string>.ComErro("LivroId não pode ser vazio."));

            try
            {
                var usuarioId = ObterUsuarioId();
                bool existe = await _listarUseCase.VerificarFavorito(usuarioId, livroId);
                return Ok(RespostaPadrao<bool>.ComSucesso(existe, "Verificação realizada com sucesso."));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(RespostaPadrao<string>.ComErro("Usuário não autorizado."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, RespostaPadrao<string>.ComErro($"Erro interno: {ex.Message}"));
            }
        }
    }
}