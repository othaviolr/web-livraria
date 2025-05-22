using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Constantes.Autor;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AutorController : ControllerBase
    {
        private readonly IAutorService _autorService;

        public AutorController(IAutorService autorService)
        {
            _autorService = autorService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var autor = await _autorService.ObterPorIdAsync(id);

            if (autor == null)
                return NotFound(RespostaPadrao<AutorDto>.ComErro(MensagensAutor.AutorNaoEncontrado));

            return Ok(RespostaPadrao<AutorDto>.ComSucesso(autor, "Autor encontrado com sucesso."));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AutorDto dto)
        {
            var autorCriado = await _autorService.AdicionarAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = autorCriado.Id },
                RespostaPadrao<AutorDto>.ComSucesso(autorCriado, MensagensAutor.AutorCriadoSucesso)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AutorDto dto)
        {
            if (id != dto.Id)
                return BadRequest(RespostaPadrao<AutorDto>.ComErro("O ID informado na URL não confere com o ID do objeto."));

            await _autorService.AtualizarAsync(dto);

            return Ok(RespostaPadrao<string>.ComSucesso(MensagensAutor.AutorAtualizadoSucesso));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _autorService.RemoverAsync(id);

            return Ok(RespostaPadrao<string>.ComSucesso(MensagensAutor.AutorRemovidoSucesso));
        }
    }
}
