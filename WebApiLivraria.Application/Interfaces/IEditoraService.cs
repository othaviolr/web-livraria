using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IEditoraService
    {
        Task<IEnumerable<EditoraDto>> ListarAsync();
        Task<EditoraDto> ObterPorIdAsync(string id);
        Task<EditoraDto> AdicionarAsync(EditoraDto dto);
        Task AtualizarAsync(EditoraDto dto);
        Task RemoverAsync(string id);
    }
}