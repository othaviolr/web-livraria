using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroDto>> ListarAsync(string? search = null);
        Task<LivroDto> ObterPorIdAsync(string id);
        Task<LivroDto> AdicionarAsync(LivroDto dto);
        Task AtualizarAsync(LivroDto dto);
        Task RemoverAsync(string id);
    }
}