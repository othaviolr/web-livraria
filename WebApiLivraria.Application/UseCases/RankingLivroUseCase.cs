using WebApiLivraria.Application.Services;
using WebApiLivraria.Application.UseCases.RankingLivro;
using WebApiLivraria.Domain.Interfaces;

namespace WebApiLivraria.Application.UseCases.RankingLivro
{
    public class RankingLivroUseCase : IRankingLivroUseCase
    {
        private readonly IRankingService _rankingService;

        public RankingLivroUseCase(IRankingService rankingService)
        {
            _rankingService = rankingService;
        }

        public async Task<List<RankingLivroResponse>> ExecutarAsync(RankingLivroRequest request)
        {
            var ranking = await _rankingService.ObterRankingAsync(request);
            return ranking;
        }
    }
}
