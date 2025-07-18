using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Repositories
{
    public interface IFavoritoRepository
    {
        Task Adicionar(Favorito favorito);
        Task Remover(string usuarioId, string livroId);
        Task<bool> Existe(string usuarioId, string livroId);
        Task<IReadOnlyCollection<Favorito>> ListarPorUsuario(string usuarioId);
    }
}