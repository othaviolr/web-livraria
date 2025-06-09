using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Repositories;

public interface IFavoritoRepository
{
    Task Adicionar(Favorito favorito);
    Task Remover(Guid usuarioId, Guid livroId);
    Task<bool> Existe(Guid usuarioId, Guid livroId);
    Task<IReadOnlyCollection<Favorito>> ListarPorUsuario(Guid usuarioId);
}