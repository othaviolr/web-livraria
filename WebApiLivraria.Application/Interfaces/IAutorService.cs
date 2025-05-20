using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface IAutorService
    {
        Task<IEnumerable<AutorDto>> ListarAsync();
        Task<AutorDto> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(AutorDto autor);
        Task AtualizarAsync(AutorDto autor);
        Task RemoverAsync(Guid id);
    }
}
