using WebApiLivraria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiLivraria.Domain.Repositories
{
    public interface IListaDesejoRepository
    {
        Task Adicionar(ListaDesejo item);
        Task Remover(Guid usuarioId, string livroId);
        Task<bool> Existe(Guid usuarioId, string livroId);
        Task<IReadOnlyCollection<ListaDesejo>> ListarPorUsuario(Guid usuarioId);
    }
}