using WebApiLivraria.Domain.Interfaces;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Excluir
{
    public class ExcluirAvaliacaoUseCase : IExcluirAvaliacaoUseCase
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public ExcluirAvaliacaoUseCase(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task ExecutarAsync(string id, string usuarioId)
        {
            var avaliacao = await _avaliacaoRepository.ObterPorIdAsync(id);
            if (avaliacao == null)
                throw new KeyNotFoundException("Avaliação não encontrada");

            if (avaliacao.UsuarioId != usuarioId.ToString())
                throw new UnauthorizedAccessException("Usuário não pode excluir essa avaliação");

            await _avaliacaoRepository.RemoverAsync(id);
        }
    }
}