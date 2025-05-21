using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IEditoraService
    {
        Task<IEnumerable<EditoraDto>> ListarAsync();
        Task<EditoraDto> ObterPorIdAsync(int id);
        Task AdicionarAsync(EditoraDto editora);
        Task AtualizarAsync(EditoraDto editora);
        Task RemoverAsync(int id);
    }
}
