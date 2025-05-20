using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IGeneroService
    {
        Task<IEnumerable<GeneroDto>> ListarAsync();
        Task<GeneroDto> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(GeneroDto genero);
        Task AtualizarAsync(GeneroDto genero);
        Task RemoverAsync(Guid id);
    }
}
