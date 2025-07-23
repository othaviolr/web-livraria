using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text.Json.Serialization;
using WebApiLivraria.Application.UseCases.ListaDesejo;

namespace WebApiLivraria.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
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

        private string ObterUsuarioId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdClaim))
                throw new UnauthorizedAccessException("Usuário não autenticado.");

            return userIdClaim;
        }

        public class AdicionarListaDesejoRequestDto
        {
            [JsonPropertyName("livroId")]
            public string LivroId { get; set; }
        }

        [HttpPost]
        public async Task<IActionResult> Adicionar([FromBody] AdicionarListaDesejoRequestDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.LivroId))
                return BadRequest(new { message = "Campo 'livroId' é obrigatório." });

            try
            {
                var usuarioId = ObterUsuarioId();

                var request = new AdicionarListaDesejoRequest
                {
                    UsuarioId = usuarioId,
                    LivroId = dto.LivroId
                };

                await _adicionarUseCase.Executar(request);

                return Ok(new { message = "Livro adicionado à lista de desejos com sucesso." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Usuário não autorizado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro inesperado no servidor: {ex.Message}" });
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Remover([FromQuery] string livroId)
        {
            if (string.IsNullOrWhiteSpace(livroId))
                return BadRequest(new { message = "Parâmetro 'livroId' é obrigatório." });

            try
            {
                var usuarioId = ObterUsuarioId();

                await _removerUseCase.Executar(usuarioId, livroId);

                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Usuário não autorizado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro inesperado no servidor: {ex.Message}" });
            }
        }

        [HttpGet]
        public async Task<IActionResult> Listar()
        {
            try
            {
                var usuarioId = ObterUsuarioId();

                var lista = await _listarUseCase.Executar(usuarioId);

                return Ok(lista);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(new { message = "Usuário não autorizado." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Erro inesperado no servidor: {ex.Message}" });
            }
        }

        [HttpGet("verificar/{livroId}")]
        public async Task<IActionResult> Verificar(string livroId)
        {
            if (string.IsNullOrWhiteSpace(livroId))
                return BadRequest(RespostaPadrao<string>.ComErro("Parâmetro 'livroId' é obrigatório."));

            try
            {
                var usuarioId = ObterUsuarioId();

                bool existe = await _listarUseCase.VerificarListaDesejo(usuarioId, livroId);

                return Ok(RespostaPadrao<bool>.ComSucesso(existe, "Verificação realizada com sucesso."));
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized(RespostaPadrao<string>.ComErro("Usuário não autorizado."));
            }
            catch (Exception ex)
            {
                return StatusCode(500, RespostaPadrao<string>.ComErro($"Erro inesperado no servidor: {ex.Message}"));
            }
        }
    }
}