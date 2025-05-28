namespace WebApiLivraria.Application.Services;

using WebApiLivraria.Application.UseCases.RankingLivro;

public interface IRankingService
{
    Task<List<RankingLivroResponse>> ObterRankingAsync(RankingLivroRequest request);
}
