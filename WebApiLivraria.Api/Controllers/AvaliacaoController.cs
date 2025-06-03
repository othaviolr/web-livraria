using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.UseCases.Avaliacao.Criar;
using WebApiLivraria.Application.UseCases.Avaliacao.Listar;
using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly ICriarAvaliacaoUseCase _criarAvaliacaoUseCase;
        private readonly IListarAvaliacoesPorLivroUseCase _listarAvaliacoesPorLivroUseCase;

        public AvaliacaoController(
            ICriarAvaliacaoUseCase criarAvaliacaoUseCase,
            IListarAvaliacoesPorLivroUseCase listarAvaliacoesPorLivroUseCase)
        {
            _criarAvaliacaoUseCase = criarAvaliacaoUseCase;
            _listarAvaliacoesPorLivroUseCase = listarAvaliacoesPorLivroUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarAvaliacaoRequest request)
        {
            await _criarAvaliacaoUseCase.ExecutarAsync(request);
            return Ok(RespostaPadrao<string>.ComSucesso("Avaliação criada com sucesso."));
        }

        [HttpGet("{livroId}")]
        public async Task<IActionResult> ListarPorLivro(int livroId)
        {
            var avaliacoes = await _listarAvaliacoesPorLivroUseCase.ExecutarAsync(livroId);
            return Ok(RespostaPadrao<List<AvaliacaoResponse>>.ComSucesso(avaliacoes));
        }
    }
}