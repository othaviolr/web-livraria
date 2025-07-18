using WebApiLivraria.Application.UseCases.RankingLivro;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Application.Services
{
    public interface IRankingService
    {
        Task<List<RankingLivro>> ObterRankingGeralAsync(int pagina, int tamanhoPagina);
        Task<List<RankingLivro>> ObterRankingPorGeneroAsync(string genero, int pagina, int tamanhoPagina);
        Task AtualizarRankingAsync();
        Task<List<RankingLivroResponse>> ObterRankingAsync(RankingLivroRequest request);
    }
}