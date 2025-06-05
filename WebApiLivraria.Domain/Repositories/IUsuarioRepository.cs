using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> ObterPorEmail(string email);
    Task Adicionar(Usuario usuario);
}