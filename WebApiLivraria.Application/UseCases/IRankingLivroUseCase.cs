namespace WebApiLivraria.Application.UseCases.RankingLivro;

public interface IRankingLivroUseCase
{
    Task<List<RankingLivroResponse>> ExecutarAsync(RankingLivroRequest request);
}
