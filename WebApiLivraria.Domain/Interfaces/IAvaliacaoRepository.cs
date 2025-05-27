using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task<IEnumerable<Avaliacao>> ListarPorLivroIdAsync(int livroId);
        Task AdicionarAsync(Avaliacao avaliacao);
        Task AtualizarAsync(Avaliacao avaliacao);
        Task RemoverAsync(int id);
    }
}
