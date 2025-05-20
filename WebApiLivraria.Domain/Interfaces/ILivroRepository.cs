using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface ILivroRepository
    {
        Task<IEnumerable<Livro>> ListarAsync();
        Task<Livro> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Livro livro);
        Task AtualizarAsync(Livro livro);
        Task RemoverAsync(Guid id);
    }
}
