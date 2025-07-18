using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IAvaliacaoRepository
    {
        Task<IEnumerable<Avaliacao>> ListarPorLivroIdAsync(string livroId);
        Task<IEnumerable<Avaliacao>> ListarPorUsuarioIdAsync(string usuarioId);

        Task AdicionarAsync(Avaliacao avaliacao);
        Task AtualizarAsync(Avaliacao avaliacao);
        Task RemoverAsync(string id);

        Task<double> ObterMediaNotasPorLivroAsync(string livroId);
        Task<int> ObterQuantidadeAvaliacoesPorLivroAsync(string livroId);

        Task<Avaliacao?> ObterPorIdAsync(string id);
    }
}