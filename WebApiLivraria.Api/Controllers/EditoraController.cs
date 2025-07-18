using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Constantes.Editora;
using System.Collections.Generic;
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
        public async Task<IActionResult> GetById(string id)
        {
            var editora = await _editoraService.ObterPorIdAsync(id);

            if (editora == null)
                return NotFound(RespostaPadrao<EditoraDto>.ComErro(MensagensEditora.EditoraNaoEncontrada));

            return Ok(RespostaPadrao<EditoraDto>.ComSucesso(editora, "Editora obtida com sucesso."));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] EditoraDto dto)
        {
            var editoraCriada = await _editoraService.AdicionarAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = editoraCriada.Id },
                RespostaPadrao<EditoraDto>.ComSucesso(editoraCriada, MensagensEditora.EditoraCriadaSucesso)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(string id, [FromBody] EditoraDto dto)
        {
            if (id != dto.Id)
                return BadRequest(RespostaPadrao<EditoraDto>.ComErro("ID inconsistente."));

            await _editoraService.AtualizarAsync(dto);

            return Ok(RespostaPadrao<string>.ComSucesso(MensagensEditora.EditoraAtualizadaSucesso));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            await _editoraService.RemoverAsync(id);

            return Ok(RespostaPadrao<string>.ComSucesso(MensagensEditora.EditoraRemovidaSucesso));
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var editoras = await _editoraService.ListarAsync();
            return Ok(RespostaPadrao<IEnumerable<EditoraDto>>.ComSucesso(editoras, "Editoras listadas com sucesso."));
        }
    }
}