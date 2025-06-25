using WebApiLivraria.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> ListarAsync(string filtro = null);

        Task<Autor> ObterPorIdAsync(int id);
        Task AdicionarAsync(Autor autor);
        Task AtualizarAsync(Autor autor);
        Task RemoverAsync(int id);

        Task<int> ObterMaiorIdAsync();
    }
}