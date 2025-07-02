using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Collections.Generic;
using WebApiLivraria.Application.UseCases.Avaliacao.Criar;
using WebApiLivraria.Application.UseCases.Avaliacao.Listar;
using WebApiLivraria.Application.UseCases.Avaliacao.Resumo;
using WebApiLivraria.Application.UseCases.Avaliacao.Editar;
using WebApiLivraria.Application.UseCases.Avaliacao.Excluir;
using WebApiLivraria.Application.Dto;
using System;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvaliacaoController : ControllerBase
    {
        private readonly ICriarAvaliacaoUseCase _criarAvaliacaoUseCase;
        private readonly IListarAvaliacoesPorLivroUseCase _listarAvaliacoesPorLivroUseCase;
        private readonly IObterResumoAvaliacaoLivroUseCase _obterResumoUseCase;
        private readonly IEditarAvaliacaoUseCase _editarAvaliacaoUseCase;
        private readonly IExcluirAvaliacaoUseCase _excluirAvaliacaoUseCase;

        public AvaliacaoController(
            ICriarAvaliacaoUseCase criarAvaliacaoUseCase,
            IListarAvaliacoesPorLivroUseCase listarAvaliacoesPorLivroUseCase,
            IObterResumoAvaliacaoLivroUseCase obterResumoUseCase,
            IEditarAvaliacaoUseCase editarAvaliacaoUseCase,
            IExcluirAvaliacaoUseCase excluirAvaliacaoUseCase)
        {
            _criarAvaliacaoUseCase = criarAvaliacaoUseCase;
            _listarAvaliacoesPorLivroUseCase = listarAvaliacoesPorLivroUseCase;
            _obterResumoUseCase = obterResumoUseCase;
            _editarAvaliacaoUseCase = editarAvaliacaoUseCase;
            _excluirAvaliacaoUseCase = excluirAvaliacaoUseCase;
        }

        [HttpPost]
        public async Task<IActionResult> Criar([FromBody] CriarAvaliacaoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _criarAvaliacaoUseCase.ExecutarAsync(request);
                return Ok(RespostaPadrao<string>.ComSucesso("Avaliação criada com sucesso."));
            }
            catch (Exception ex)
            {
                return BadRequest(RespostaPadrao<string>.ComErro($"Erro ao criar avaliação: {ex.Message}"));
            }
        }

        [HttpGet("livro/{livroId}")]
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

        [HttpPut("{id}")]
        public async Task<IActionResult> Editar(int id, [FromBody] EditarAvaliacaoRequest request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id != request.Id)
                return BadRequest(RespostaPadrao<string>.ComErro("Id da avaliação inconsistente."));

            try
            {
                await _editarAvaliacaoUseCase.ExecutarAsync(request);
                return Ok(RespostaPadrao<string>.ComSucesso("Avaliação atualizada com sucesso."));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(RespostaPadrao<string>.ComErro(knf.Message));
            }
            catch (UnauthorizedAccessException ua)
            {
                return Unauthorized(RespostaPadrao<string>.ComErro(ua.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(RespostaPadrao<string>.ComErro($"Erro ao atualizar avaliação: {ex.Message}"));
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Excluir(int id, [FromQuery] Guid usuarioId)
        {
            try
            {
                await _excluirAvaliacaoUseCase.ExecutarAsync(id, usuarioId);
                return Ok(RespostaPadrao<string>.ComSucesso("Avaliação excluída com sucesso."));
            }
            catch (KeyNotFoundException knf)
            {
                return NotFound(RespostaPadrao<string>.ComErro(knf.Message));
            }
            catch (UnauthorizedAccessException ua)
            {
                return Unauthorized(RespostaPadrao<string>.ComErro(ua.Message));
            }
            catch (Exception ex)
            {
                return BadRequest(RespostaPadrao<string>.ComErro($"Erro ao excluir avaliação: {ex.Message}"));
            }
        }
    }
}