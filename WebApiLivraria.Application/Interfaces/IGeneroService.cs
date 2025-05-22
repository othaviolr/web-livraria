using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroDto>> ListarAsync();
        Task<GeneroDto> ObterPorIdAsync(int id);
        Task<GeneroDto> AdicionarAsync(GeneroDto dto);
        Task AtualizarAsync(GeneroDto dto);
        Task RemoverAsync(int id);
    }
}
