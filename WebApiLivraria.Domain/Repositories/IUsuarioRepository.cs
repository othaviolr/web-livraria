using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Enums;

namespace WebApiLivraria.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorEmail(string email);
        Task<Usuario?> ObterPorId(Guid id);
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task<Usuario?> ObterPorIdComAvaliacoesAsync(Guid id);
        Task<Usuario?> ObterPorNomeUsuarioComRelacionamentosAsync(string nomeUsuario);
        Task Remover(Usuario usuario);
        Task<Usuario?> ObterPorIdComDetalhesAsync(Guid id);

        Task<Dictionary<StatusLeitura, int>> ObterContagemLivrosPorStatusAsync(Guid usuarioId);

        Task<int> ObterQuantidadeFavoritosAsync(Guid usuarioId);
    }
}