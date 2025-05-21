using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using System.Threading.Tasks;

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

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var livro = await _livroService.ObterPorIdAsync(id);
            return livro == null ? NotFound() : Ok(livro);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LivroDto dto)
        {
            await _livroService.AdicionarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LivroDto dto)
        {
            if (id != dto.Id) return BadRequest("ID do livro não confere.");
            await _livroService.AtualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _livroService.RemoverAsync(id);
            return NoContent();
        }
    }
}
