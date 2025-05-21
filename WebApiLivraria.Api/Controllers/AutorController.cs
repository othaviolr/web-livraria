using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using System.Threading.Tasks;

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
            return autor == null ? NotFound() : Ok(autor);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AutorDto dto)
        {
            await _autorService.AdicionarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] AutorDto dto)
        {
            if (id != dto.Id) return BadRequest("ID inconsistente.");
            await _autorService.AtualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _autorService.RemoverAsync(id);
            return NoContent();
        }
    }
}