using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> ListarAsync();
        Task<Genero> ObterPorIdAsync(int id);
        Task AdicionarAsync(Genero genero);
        Task AtualizarAsync(Genero genero);
        Task RemoverAsync(int id);
    }
}
