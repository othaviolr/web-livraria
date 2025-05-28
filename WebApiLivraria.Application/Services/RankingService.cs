using WebApiLivraria.Application.UseCases.RankingLivro;

namespace WebApiLivraria.Application.Services;

public class RankingService : IRankingService
{
    private readonly IRankingLivroUseCase _rankingLivroUseCase;

    public RankingService(IRankingLivroUseCase rankingLivroUseCase)
    {
        _rankingLivroUseCase = rankingLivroUseCase;
    }

    public async Task<List<RankingLivroResponse>> ObterRankingAsync(RankingLivroRequest request)
    {
        return await _rankingLivroUseCase.ExecutarAsync(request);
    }
}
