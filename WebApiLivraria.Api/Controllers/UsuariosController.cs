using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiLivraria.Application.Requests.UsuarioSeguindo;
using WebApiLivraria.Application.UseCases.UsuarioSeguindo;
using WebApiLivraria.Application.UseCases.UsuarioSeguindo.ObterSeguidores;
using WebApiLivraria.Application.UseCases.UsuarioSeguindo.ObterSeguindo;
using WebApiLivraria.Application.UseCases.Usuarios.AtualizarPerfil;
using WebApiLivraria.Application.UseCases.Usuarios.Excluir;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilCompletoUseCase;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico;
using WebApiLivraria.Application.UseCases.Leitura.Resumo;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Application.UseCases.Usuarios;

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
        private readonly SeguirUsuarioHandler _seguirUsuarioHandler;
        private readonly DeixarDeSeguirUsuarioHandler _deixarDeSeguirUsuarioHandler;
        private readonly ObterSeguidoresHandler _obterSeguidoresHandler;
        private readonly ObterSeguindoHandler _obterSeguindoHandler;

        public UsuariosController(
            IUsuarioRepository usuarioRepository,
            ObterPerfilCompletoUseCase obterPerfilCompletoUseCase,
            IObterPerfilPublicoUseCase obterPerfilPublicoUseCase,
            AtualizarPerfilUseCase atualizarPerfilUseCase,
            ExcluirUsuarioUseCase excluirUsuarioUseCase,
            ObterResumoStatusLeituraUseCase obterResumoStatusLeituraUseCase,
            SeguirUsuarioHandler seguirUsuarioHandler,
            DeixarDeSeguirUsuarioHandler deixarDeSeguirUsuarioHandler,
            ObterSeguidoresHandler obterSeguidoresHandler,
            ObterSeguindoHandler obterSeguindoHandler
        )
        {
            _usuarioRepository = usuarioRepository;
            _obterPerfilCompletoUseCase = obterPerfilCompletoUseCase;
            _obterPerfilPublicoUseCase = obterPerfilPublicoUseCase;
            _atualizarPerfilUseCase = atualizarPerfilUseCase;
            _excluirUsuarioUseCase = excluirUsuarioUseCase;
            _obterResumoStatusLeituraUseCase = obterResumoStatusLeituraUseCase;
            _seguirUsuarioHandler = seguirUsuarioHandler;
            _deixarDeSeguirUsuarioHandler = deixarDeSeguirUsuarioHandler;
            _obterSeguidoresHandler = obterSeguidoresHandler;
            _obterSeguindoHandler = obterSeguindoHandler;
        }

        private bool TryObterUsuarioIdDoToken(out string userId)
        {
            var userIdClaim = User.Claims.FirstOrDefault(c =>
                c.Type == "id" ||
                c.Type == ClaimTypes.NameIdentifier ||
                c.Type == "sub"
            );

            if (userIdClaim == null || string.IsNullOrWhiteSpace(userIdClaim.Value))
            {
                userId = string.Empty;
                return false;
            }

            userId = userIdClaim.Value;
            return true;
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
            if (!TryObterUsuarioIdDoToken(out var userId))
                return Unauthorized();

            var perfilDto = await _obterPerfilCompletoUseCase.ExecutarAsync(userId);
            if (perfilDto == null)
                return NotFound();

            return Ok(perfilDto);
        }

        [HttpPut("perfil")]
        [Authorize]
        public async Task<IActionResult> AtualizarPerfil([FromBody] AtualizarPerfilRequest request)
        {
            if (!TryObterUsuarioIdDoToken(out var userId))
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
            if (!TryObterUsuarioIdDoToken(out var userId))
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
            if (!TryObterUsuarioIdDoToken(out var userId))
                return Unauthorized();

            var resumo = await _obterResumoStatusLeituraUseCase.ExecutarAsync(userId);
            return Ok(resumo);
        }

        [HttpPost("{id}/seguir")]
        [Authorize]
        public async Task<IActionResult> SeguirUsuario(Guid id)
        {
            if (!TryObterUsuarioIdDoToken(out var usuarioLogadoId))
                return Unauthorized();

            var request = new SeguirUsuarioRequest
            {
                UsuarioIdParaSeguir = id.ToString()
            };

            await _seguirUsuarioHandler.HandleAsync(usuarioLogadoId.ToString(), request);

            return NoContent();
        }

        [HttpDelete("{id}/deixar-de-seguir")]
        [Authorize]
        public async Task<IActionResult> DeixarDeSeguirUsuario(string id)
        {
            if (!TryObterUsuarioIdDoToken(out var usuarioLogadoId))
                return Unauthorized();

            var request = new DeixarDeSeguirRequest
            {
                UsuarioIdParaDeixarDeSeguir = id
            };

            await _deixarDeSeguirUsuarioHandler.HandleAsync(usuarioLogadoId, request);

            return NoContent();
        }

        [HttpGet("{id}/seguidores")]
        [Authorize]
        public async Task<IActionResult> ObterSeguidores(string id)
        {
            var request = new ObterSeguidoresRequest(id);
            var seguidores = await _obterSeguidoresHandler.HandleAsync(request);
            return Ok(seguidores);
        }

        [HttpGet("{id}/seguindo")]
        [Authorize]
        public async Task<IActionResult> ObterSeguindo(string id)
        {
            var request = new ObterSeguindoRequest(id);
            var seguindo = await _obterSeguindoHandler.HandleAsync(request);
            return Ok(seguindo);
        }
    }
}