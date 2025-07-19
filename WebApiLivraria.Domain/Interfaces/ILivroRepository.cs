using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ListarAsync();
        Task<Livro> ObterPorIdAsync(string id);
        Task AdicionarAsync(Livro livro);
        Task AtualizarAsync(Livro livro);
        Task RemoverAsync(string id);
        Task<IEnumerable<Livro>> ListarComFiltroAsync(string? search);
        Task<IEnumerable<Livro>> ObterLivrosComFiltroAsync(int? anoPublicacao, string? genero);
    }
}