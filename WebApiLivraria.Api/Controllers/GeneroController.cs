using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Constantes.Genero;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GeneroController : ControllerBase
    {
        private readonly IGeneroService _generoService;

        public GeneroController(IGeneroService generoService)
        {
            _generoService = generoService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(string id)
        {
            var genero = await _generoService.ObterPorIdAsync(id);

            if (genero == null)
                return NotFound(RespostaPadrao<GeneroDto>.ComErro(MensagensGenero.GeneroNaoEncontrado));

            return Ok(RespostaPadrao<GeneroDto>.ComSucesso(genero, "Gênero obtido com sucesso."));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GeneroDto dto)
        {
            var generoCriado = await _generoService.AdicionarAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = generoCriado.Id },
                RespostaPadrao<GeneroDto>.ComSucesso(generoCriado, MensagensGenero.GeneroCriadoSucesso)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] GeneroDto dto)
        {
            if (id != dto.Id)
                return BadRequest(RespostaPadrao<GeneroDto>.ComErro("ID inconsistente."));

            await _generoService.AtualizarAsync(dto);

            return Ok(RespostaPadrao<string>.ComSucesso(MensagensGenero.GeneroAtualizadoSucesso));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _generoService.RemoverAsync(id);

            return Ok(RespostaPadrao<string>.ComSucesso(MensagensGenero.GeneroRemovidoSucesso));
        }
    }
}