using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmail(string email);
    Task<Usuario?> ObterPorId(Guid id);
    Task Adicionar(Usuario usuario);
    Task Atualizar(Usuario usuario);  
    Task<Usuario?> ObterPorIdComAvaliacoesAsync(Guid id);
}