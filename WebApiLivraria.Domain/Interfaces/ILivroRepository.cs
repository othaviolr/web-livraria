using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ListarAsync();
        Task<Livro> ObterPorIdAsync(int id);
        Task AdicionarAsync(Livro livro);
        Task AtualizarAsync(Livro livro);
        Task RemoverAsync(int id);
        Task<IEnumerable<Livro>> ListarComFiltroAsync(string? search);
        Task<IEnumerable<Livro>> ObterLivrosComFiltroAsync(int? ano, string? genero);

    }
}