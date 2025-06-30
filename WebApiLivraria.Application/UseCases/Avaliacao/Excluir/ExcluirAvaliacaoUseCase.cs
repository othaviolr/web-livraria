using WebApiLivraria.Domain.Interfaces;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Excluir
{
    public class ExcluirAvaliacaoUseCase : IExcluirAvaliacaoUseCase
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public ExcluirAvaliacaoUseCase(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task ExecutarAsync(int id, int usuarioId)
        {
            var avaliacao = await _avaliacaoRepository.ObterPorIdAsync(id);
            if (avaliacao == null)
                throw new KeyNotFoundException("Avaliação não encontrada");

            if (avaliacao.UsuarioId != usuarioId)
                throw new UnauthorizedAccessException("Usuário não pode excluir essa avaliação");

            await _avaliacaoRepository.RemoverAsync(id);
        }
    }
}
