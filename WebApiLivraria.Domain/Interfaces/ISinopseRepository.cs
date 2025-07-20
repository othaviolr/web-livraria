using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface ISinopseRepository
    {
        Task<Sinopse?> ObterPorLivroIdAsync(string livroId);
        Task<IEnumerable<Sinopse>> ListarPorLivroIdsAsync(IEnumerable<string> livroIds);
        Task AdicionarAsync(Sinopse sinopse);
        Task AtualizarAsync(Sinopse sinopse);
        Task RemoverPorLivroIdAsync(string livroId);
    }
}