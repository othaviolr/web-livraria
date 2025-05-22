using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IEditoraService
    {
        Task<IEnumerable<EditoraDto>> ListarAsync();
        Task<EditoraDto> ObterPorIdAsync(int id);
        Task AdicionarAsync(EditoraDto dto);
        Task AtualizarAsync(EditoraDto dto);
        Task RemoverAsync(int id);
    }
}
