using WebApiLivraria.Domain.Interfaces;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Resumo
{
    public class ObterResumoAvaliacaoLivroUseCase : IObterResumoAvaliacaoLivroUseCase
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public ObterResumoAvaliacaoLivroUseCase(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<ResumoAvaliacaoLivroResponse> ExecutarAsync(string livroId)
        {
            var notaMedia = await _avaliacaoRepository.ObterMediaNotasPorLivroAsync(livroId);
            var totalAvaliacoes = await _avaliacaoRepository.ObterQuantidadeAvaliacoesPorLivroAsync(livroId);

            return new ResumoAvaliacaoLivroResponse
            {
                NotaMedia = Math.Round(notaMedia, 1),
                TotalAvaliacoes = totalAvaliacoes
            };
        }
    }
}