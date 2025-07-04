using System;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.UseCases.Leitura.Atualizar;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class LeituraController : ControllerBase
    {
        private readonly AtualizarLeituraUseCase _atualizarLeituraUseCase;

        public LeituraController(AtualizarLeituraUseCase atualizarLeituraUseCase)
        {
            _atualizarLeituraUseCase = atualizarLeituraUseCase;
        }

        [HttpPost("status")]
        public async Task<IActionResult> AtualizarStatus([FromBody] AtualizarLeituraDto dto)
        {
            var usuarioIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (!Guid.TryParse(usuarioIdString, out var usuarioId))
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
    }
}