using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilCompletoUseCase;
using WebApiLivraria.Application.UseCases.Usuarios;
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

        public UsuariosController(
            IUsuarioRepository usuarioRepository,
            ObterPerfilCompletoUseCase obterPerfilCompletoUseCase)
        {
            _usuarioRepository = usuarioRepository;
            _obterPerfilCompletoUseCase = obterPerfilCompletoUseCase;
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
            if (userId == Guid.Empty) return Unauthorized();

            var usuario = await _usuarioRepository.ObterPorId(userId);
            if (usuario == null) return NotFound();

            usuario.AtualizarPerfil(request.NomeUsuario, request.FotoUrl, request.Cidade, request.Role);

            await _usuarioRepository.Atualizar(usuario);

            var perfilDto = new UsuarioPerfilDto
            {
                NomeUsuario = usuario.NomeUsuario,
                FotoUrl = usuario.FotoUrl,
                Cidade = usuario.Cidade,
                Role = usuario.Role
            };

            return Ok(perfilDto);
        }
    }
}