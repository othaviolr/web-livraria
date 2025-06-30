using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.UseCases.Avaliacao.Criar;
using WebApiLivraria.Application.UseCases.Avaliacao.Listar;
using WebApiLivraria.Application.UseCases.Avaliacao.Resumo;
using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly ICriarAvaliacaoUseCase _criarAvaliacaoUseCase;
        private readonly IListarAvaliacoesPorLivroUseCase _listarAvaliacoesPorLivroUseCase;
        private readonly IObterResumoAvaliacaoLivroUseCase _obterResumoUseCase;

        public AvaliacaoController(
            ICriarAvaliacaoUseCase criarAvaliacaoUseCase,
            IListarAvaliacoesPorLivroUseCase listarAvaliacoesPorLivroUseCase,
            IObterResumoAvaliacaoLivroUseCase obterResumoUseCase)
        {
            _criarAvaliacaoUseCase = criarAvaliacaoUseCase;
            _listarAvaliacoesPorLivroUseCase = listarAvaliacoesPorLivroUseCase;
            _obterResumoUseCase = obterResumoUseCase;
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

        [HttpGet("livro/{livroId}/resumo")]
        public async Task<IActionResult> ObterResumo(int livroId)
        {
            var resumo = await _obterResumoUseCase.ExecutarAsync(livroId);
            return Ok(RespostaPadrao<ResumoAvaliacaoLivroResponse>.ComSucesso(resumo));
        }
    }
}