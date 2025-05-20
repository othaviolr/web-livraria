using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> ListarAsync();
        Task<Genero> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(Genero genero);
        Task AtualizarAsync(Genero genero);
        Task RemoverAsync(Guid id);
    }
}
