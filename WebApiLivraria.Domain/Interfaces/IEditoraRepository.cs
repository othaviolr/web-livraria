using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Interfaces
{
    public interface IEditoraRepository
    {
        Task<IEnumerable<Editora>> ListarAsync();
        Task<Editora> ObterPorIdAsync(int id);
        Task AdicionarAsync(Editora editora);
        Task AtualizarAsync(Editora editora);
        Task RemoverAsync(int id);
    }
}
