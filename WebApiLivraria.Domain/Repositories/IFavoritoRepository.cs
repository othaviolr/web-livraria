using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Repositories
{
    public interface IFavoritoRepository
    {
        Task Adicionar(Favorito favorito);
        Task Remover(Guid usuarioId, int livroId);
        Task<bool> Existe(Guid usuarioId, int livroId);
        Task<IReadOnlyCollection<Favorito>> ListarPorUsuario(Guid usuarioId);
    }
}