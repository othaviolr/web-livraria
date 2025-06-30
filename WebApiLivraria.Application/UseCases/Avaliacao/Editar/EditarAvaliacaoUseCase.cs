using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Interfaces;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Editar
{
    public class EditarAvaliacaoUseCase : IEditarAvaliacaoUseCase
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public EditarAvaliacaoUseCase(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task ExecutarAsync(EditarAvaliacaoRequest request)
        {
            var avaliacao = await _avaliacaoRepository.ObterPorIdAsync(request.Id);
            if (avaliacao == null)
                throw new KeyNotFoundException("Avaliação não encontrada");

            if (avaliacao.UsuarioId != request.UsuarioId)
                throw new UnauthorizedAccessException("Usuário não pode editar essa avaliação");

            avaliacao.Atualizar(request.Nota, request.Comentario);

            await _avaliacaoRepository.AtualizarAsync(avaliacao);
        }
    }
}