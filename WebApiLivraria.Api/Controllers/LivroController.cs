using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class LivroController : ControllerBase
    {
        private readonly ILivroService _livroService;

        public LivroController(ILivroService livroService)
        {
            _livroService = livroService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var livros = await _livroService.ListarAsync();
            return Ok(livros);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var livro = await _livroService.ObterPorIdAsync(id);
            if (livro == null)
                return NotFound();

            return Ok(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LivroDto dto)
        {
            await _livroService.AdicionarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] LivroDto dto)
        {
            if (id != dto.Id)
                return BadRequest("ID do livro não confere.");

            await _livroService.AtualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _livroService.RemoverAsync(id);
            return NoContent();
        }
    }
}
