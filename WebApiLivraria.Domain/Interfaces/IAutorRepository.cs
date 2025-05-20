using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> ListarAsync();
        Task<Autor> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Autor autor);
        Task AtualizarAsync(Autor autor);
        Task RemoverAsync(Guid id);
    }
}
