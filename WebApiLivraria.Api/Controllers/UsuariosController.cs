using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiLivraria.Application.UseCases.Usuarios;
using WebApiLivraria.Application.UseCases.Usuarios.AtualizarPerfil;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilCompletoUseCase;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico;
using WebApiLivraria.Application.UseCases.Usuarios.Excluir;
using WebApiLivraria.Application.UseCases.Leitura.Resumo;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("usuarios")]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ObterPerfilCompletoUseCase _obterPerfilCompletoUseCase;
        private readonly IObterPerfilPublicoUseCase _obterPerfilPublicoUseCase;
        private readonly AtualizarPerfilUseCase _atualizarPerfilUseCase;
        private readonly ExcluirUsuarioUseCase _excluirUsuarioUseCase;
        private readonly ObterResumoStatusLeituraUseCase _obterResumoStatusLeituraUseCase;

        public UsuariosController(
            IUsuarioRepository usuarioRepository,
            ObterPerfilCompletoUseCase obterPerfilCompletoUseCase,
            IObterPerfilPublicoUseCase obterPerfilPublicoUseCase,
            AtualizarPerfilUseCase atualizarPerfilUseCase,
            ExcluirUsuarioUseCase excluirUsuarioUseCase,
            ObterResumoStatusLeituraUseCase obterResumoStatusLeituraUseCase // <- injetado
        )
        {
            _usuarioRepository = usuarioRepository;
            _obterPerfilCompletoUseCase = obterPerfilCompletoUseCase;
            _obterPerfilPublicoUseCase = obterPerfilPublicoUseCase;
            _atualizarPerfilUseCase = atualizarPerfilUseCase;
            _excluirUsuarioUseCase = excluirUsuarioUseCase;
            _obterResumoStatusLeituraUseCase = obterResumoStatusLeituraUseCase;
        }

        private Guid ObterUsuarioIdDoToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == "id" ||
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "sub"
            );

            return userIdClaim == null ? Guid.Empty : Guid.Parse(userIdClaim.Value);
        }

        [HttpGet("{nomeUsuario}")]
        [AllowAnonymous]
        public async Task<IActionResult> ObterPerfilPublico(string nomeUsuario)
        {
            var perfilPublico = await _obterPerfilPublicoUseCase.ExecutarAsync(nomeUsuario);
            if (perfilPublico == null)
                return NotFound(new { mensagem = "Usuário não encontrado." });

            return Ok(perfilPublico);
        }

        [HttpGet("perfil")]
        [Authorize]
        public async Task<IActionResult> ObterPerfil()
        {
            try
            {
                var userId = ObterUsuarioIdDoToken();
                if (userId == Guid.Empty)
                    return Unauthorized();

                var perfilDto = await _obterPerfilCompletoUseCase.ExecutarAsync(userId);
                if (perfilDto == null)
                    return NotFound();

                return Ok(perfilDto);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    Sucesso = false,
                    Mensagem = "Erro interno: " + ex.Message,
                    Stack = ex.StackTrace
                });
            }
        }

        [HttpPut("perfil")]
        [Authorize]
        public async Task<IActionResult> AtualizarPerfil([FromBody] AtualizarPerfilRequest request)
        {
            var userId = ObterUsuarioIdDoToken();
            if (userId == Guid.Empty)
                return Unauthorized();

            var sucesso = await _atualizarPerfilUseCase.ExecutarAsync(userId, request);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }

        [HttpDelete("perfil")]
        [Authorize]
        public async Task<IActionResult> ExcluirPerfil()
        {
            var userId = ObterUsuarioIdDoToken();
            if (userId == Guid.Empty)
                return Unauthorized();

            var sucesso = await _excluirUsuarioUseCase.ExecutarAsync(userId);
            if (!sucesso)
                return NotFound();

            return NoContent();
        }

        [HttpGet("resumo-leitura")]
        [Authorize]
        public async Task<IActionResult> ObterResumoLeitura()
        {
            var userId = ObterUsuarioIdDoToken();
            if (userId == Guid.Empty)
                return Unauthorized();

            var resumo = await _obterResumoStatusLeituraUseCase.ExecutarAsync(userId);

            return Ok(resumo);
        }
    }
}