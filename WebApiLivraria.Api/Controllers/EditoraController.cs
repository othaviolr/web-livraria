using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using System.Threading.Tasks;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EditoraController : ControllerBase
    {
        private readonly IEditoraService _editoraService;

        public EditoraController(IEditoraService editoraService)
        {
            _editoraService = editoraService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var editora = await _editoraService.ObterPorIdAsync(id);
            return editora == null ? NotFound() : Ok(editora);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EditoraDto dto)
        {
            await _editoraService.AdicionarAsync(dto);
            return CreatedAtAction(nameof(GetById), new { id = dto.Id }, dto);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] EditoraDto dto)
        {
            if (id != dto.Id) return BadRequest("ID inconsistente.");
            await _editoraService.AtualizarAsync(dto);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _editoraService.RemoverAsync(id);
            return NoContent();
        }
    }
}
