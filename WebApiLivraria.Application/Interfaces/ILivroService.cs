using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroDto>> ListarAsync();
        Task<LivroDto> ObterPorIdAsync(Guid id);
        Task AdicionarAsync(LivroDto livro);
        Task AtualizarAsync(LivroDto livro);
        Task RemoverAsync(Guid id);
    }
}
