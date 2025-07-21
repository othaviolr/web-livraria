using WebApiLivraria.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiLivraria.Domain.Repositories
{
    public interface IListaDesejoRepository
    {
        Task Adicionar(ListaDesejo item);
        Task Remover(string usuarioId, string livroId);
        Task<bool> Existe(string usuarioId, string livroId);
        Task<IReadOnlyCollection<ListaDesejo>> ListarPorUsuario(string usuarioId);
    }
}