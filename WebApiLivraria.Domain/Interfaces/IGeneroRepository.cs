using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IGeneroRepository
    {
        Task<IEnumerable<Genero>> ListarAsync();
        Task<Genero?> ObterPorIdAsync(string id);
        Task AdicionarAsync(Genero genero);
        Task AtualizarAsync(Genero genero);
        Task RemoverAsync(string id);

        Task<IEnumerable<Genero>> ListarPorIdsAsync(IEnumerable<string> ids);
    }
}