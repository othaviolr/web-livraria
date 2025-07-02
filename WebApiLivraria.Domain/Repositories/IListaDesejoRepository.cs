using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Repositories;

public interface IListaDesejoRepository
{
    Task Adicionar(ListaDesejo item);
    Task Remover(Guid usuarioId, int livroId);      
    Task<bool> Existe(Guid usuarioId, int livroId); 
    Task<IReadOnlyCollection<ListaDesejo>> ListarPorUsuario(Guid usuarioId);
}