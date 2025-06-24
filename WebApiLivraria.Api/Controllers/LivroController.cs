using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Constantes.Livro;

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

            if (livro == null)
                return NotFound(RespostaPadrao<LivroDto>.ComErro(MensagensLivro.LivroNaoEncontrado));

            return Ok(RespostaPadrao<LivroDto>.ComSucesso(livro, "Livro obtido com sucesso."));
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] string? search = null)
        {
            var livros = await _livroService.ListarAsync(search);
            return Ok(RespostaPadrao<IEnumerable<LivroDto>>.ComSucesso(livros, "Livros listados com sucesso."));
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] LivroDto dto)
        {
            var livroCriado = await _livroService.AdicionarAsync(dto);

            return CreatedAtAction(
                nameof(GetById),
                new { id = livroCriado.Id },
                RespostaPadrao<LivroDto>.ComSucesso(livroCriado, MensagensLivro.LivroCriadoSucesso)
            );
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] LivroDto dto)
        {
            if (id != dto.Id)
                return BadRequest(RespostaPadrao<string>.ComErro("O ID informado na URL não confere com o ID do objeto."));

            await _livroService.AtualizarAsync(dto);

            return Ok(RespostaPadrao<string>.ComSucesso(MensagensLivro.LivroAtualizadoSucesso));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _livroService.RemoverAsync(id);

            return Ok(RespostaPadrao<string>.ComSucesso(MensagensLivro.LivroRemovidoSucesso));
        }
    }
}