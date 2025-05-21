using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using System.Threading.Tasks;

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
        public async Task<IActionResult> GetById(int id)
        {
            var genero = await _generoService.ObterPorIdAsync(id);
            return genero == null ? NotFound() : Ok(genero);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] GeneroDto dto)
        {
            await _generoService.AdicionarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] GeneroDto dto)
        {
            if (id != dto.Id) return BadRequest("ID inconsistente.");
            await _generoService.AtualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _generoService.RemoverAsync(id);
            return NoContent();
        }
    }
}
