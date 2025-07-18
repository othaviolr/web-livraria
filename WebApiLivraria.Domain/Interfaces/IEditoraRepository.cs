using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IEditoraRepository
    {
        Task<IEnumerable<Editora>> ListarAsync();
        Task<Editora?> ObterPorIdAsync(string id);
        Task AdicionarAsync(Editora editora);
        Task AtualizarAsync(Editora editora);
        Task RemoverAsync(string id);
    }
}