using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface ILivroGeneroRepository
    {
        Task AtualizarGenerosDoLivroAsync(string livroId, IEnumerable<string> generoIds);
        Task<List<LivroGenero>> ObterPorLivroIdAsync(string livroId);
        Task RemoverPorLivroIdAsync(string livroId);
    }
}