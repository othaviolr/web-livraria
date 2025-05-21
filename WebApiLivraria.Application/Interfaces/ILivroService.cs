using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.Interfaces
{
    public interface ILivroService
    {
        Task<IEnumerable<LivroDto>> ListarAsync();
        Task<LivroDto> ObterPorIdAsync(int id);
        Task AdicionarAsync(LivroDto livro);
        Task AtualizarAsync(LivroDto livro);
        Task RemoverAsync(int id);
    }
}
