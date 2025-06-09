using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Repositories;

public interface IListaDesejoRepository
{
    Task Adicionar(ListaDesejo item);
    Task Remover(Guid usuarioId, Guid livroId);
    Task<bool> Existe(Guid usuarioId, Guid livroId);
    Task<IReadOnlyCollection<ListaDesejo>> ListarPorUsuario(Guid usuarioId);
}