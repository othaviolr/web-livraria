using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiLivraria.Application.UseCases.Usuarios;
using WebApiLivraria.Application.UseCases.Usuarios.AtualizarPerfil;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilCompletoUseCase;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("usuarios")]
    [Authorize]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ObterPerfilCompletoUseCase _obterPerfilCompletoUseCase;
        private readonly AtualizarPerfilUseCase _atualizarPerfilUseCase;

        public UsuariosController(
            IUsuarioRepository usuarioRepository,
            ObterPerfilCompletoUseCase obterPerfilCompletoUseCase,
            AtualizarPerfilUseCase atualizarPerfilUseCase)
        {
            _usuarioRepository = usuarioRepository;
            _obterPerfilCompletoUseCase = obterPerfilCompletoUseCase;
            _atualizarPerfilUseCase = atualizarPerfilUseCase;
        }

        private Guid ObterUsuarioIdDoToken()
        {
            var userIdClaim = User.Claims.FirstOrDefault(c => c.Type == "id");
            return userIdClaim == null ? Guid.Empty : Guid.Parse(userIdClaim.Value);
        }

        [HttpGet("perfil")]
        public async Task<IActionResult> ObterPerfil()
        {
            var userId = ObterUsuarioIdDoToken();
            if (userId == Guid.Empty)
                return Unauthorized();

            var perfilDto = await _obterPerfilCompletoUseCase.ExecutarAsync(userId);

            if (perfilDto == null)
                return NotFound();

            return Ok(perfilDto);
        }

        [HttpPut("perfil")]
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
    }
}