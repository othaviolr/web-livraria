using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.UseCases.Leitura.Atualizar;
using WebApiLivraria.Application.UseCases.Leitura.Listar;
using WebApiLivraria.Application.UseCases.Leitura.Remover;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LeituraController : ControllerBase
    {
        private readonly AtualizarLeituraUseCase _atualizarLeituraUseCase;
        private readonly ObterLeiturasUseCase _obterLeiturasUseCase;
        private readonly RemoverLeituraUseCase _removerLeituraUseCase;

        public LeituraController(
            AtualizarLeituraUseCase atualizarLeituraUseCase,
            ObterLeiturasUseCase obterLeiturasUseCase,
            RemoverLeituraUseCase removerLeituraUseCase)
        {
            _atualizarLeituraUseCase = atualizarLeituraUseCase;
            _obterLeiturasUseCase = obterLeiturasUseCase;
            _removerLeituraUseCase = removerLeituraUseCase;
        }

        [HttpPut]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarLeituraDto dto)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized("Usuário inválido");

            var leitura = await _atualizarLeituraUseCase.ExecutarAsync(usuarioId, dto);

            return Ok(new
            {
                leitura.Id,
                leitura.LivroId,
                leitura.Status,
                leitura.DataAtualizacao
            });
        }

        [HttpGet]
        public async Task<IActionResult> ListarLeituras()
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized("Usuário inválido");

            var leituras = await _obterLeiturasUseCase.ExecutarAsync(usuarioId);

            return Ok(leituras);
        }

        [HttpDelete("{livroId}")]
        public async Task<IActionResult> RemoverLeitura(string livroId)
        {
            var usuarioId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (string.IsNullOrEmpty(usuarioId))
                return Unauthorized("Usuário inválido");

            var sucesso = await _removerLeituraUseCase.ExecutarAsync(usuarioId, livroId);

            if (!sucesso)
                return NotFound("Marcações para esse livro não foram encontradas.");

            return NoContent();
        }
    }
}