using WebApiLivraria.Application.Dto;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDto>> ListarAsync();
        Task<AutorDto> ObterPorIdAsync(int id);
        Task<AutorDto> AdicionarAsync(AutorDto autor);
        Task AtualizarAsync(AutorDto autor);
        Task RemoverAsync(int id);
    }
}
