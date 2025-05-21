using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> ListarAsync();
        Task<Autor> ObterPorIdAsync(int id);
        Task AdicionarAsync(Autor autor);
        Task AtualizarAsync(Autor autor);
        Task RemoverAsync(int id);
    }
}
