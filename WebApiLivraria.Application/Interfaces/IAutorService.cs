using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDto>> ListarAsync();
        Task<AutorDto?> ObterPorIdAsync(string id);
        Task<AutorDto> AdicionarAsync(AutorDto autor);
        Task AtualizarAsync(AutorDto autor);
        Task RemoverAsync(string id);
        Task<List<AutorDto>> ObterTodosAsync(string filtro = null, string? editoraId = null);
    }
}