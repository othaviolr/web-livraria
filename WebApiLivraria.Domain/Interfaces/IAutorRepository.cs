using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IAutorRepository
    {
        Task<IEnumerable<Autor>> ListarAsync(string filtro = null, int? editoraId = null);
        Task<Autor> ObterPorIdAsync(int id);
        Task<Autor> AdicionarAsync(Autor autor);
        Task AtualizarAsync(Autor autor);
        Task RemoverAsync(int id);
    }
}