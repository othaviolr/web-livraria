using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroDto>> ListarAsync();
        Task<GeneroDto> ObterPorIdAsync(string id);
        Task<GeneroDto> AdicionarAsync(GeneroDto dto);
        Task AtualizarAsync(GeneroDto dto);
        Task RemoverAsync(string id);
    }
}