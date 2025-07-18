using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class FavoritoRepository : IFavoritoRepository
    {
        private readonly IMongoCollection<Favorito> _favoritos;

        public FavoritoRepository(MongoDbContext context)
        {
            _favoritos = context.Favoritos;
        }

        public async Task Adicionar(Favorito favorito)
        {
            await _favoritos.InsertOneAsync(favorito);
        }

        public async Task Remover(string usuarioId, string livroId)
        {
            var filter = Builders<Favorito>.Filter.And(
                Builders<Favorito>.Filter.Eq(f => f.UsuarioId, usuarioId),
                Builders<Favorito>.Filter.Eq(f => f.LivroId, livroId)
            );

            await _favoritos.DeleteOneAsync(filter);
        }

        public async Task<bool> Existe(string usuarioId, string livroId)
        {
            var filter = Builders<Favorito>.Filter.And(
                Builders<Favorito>.Filter.Eq(f => f.UsuarioId, usuarioId),
                Builders<Favorito>.Filter.Eq(f => f.LivroId, livroId)
            );

            return await _favoritos.Find(filter).AnyAsync();
        }

        public async Task<IReadOnlyCollection<Favorito>> ListarPorUsuario(string usuarioId)
        {
            var filter = Builders<Favorito>.Filter.Eq(f => f.UsuarioId, usuarioId);
            var favoritos = await _favoritos.Find(filter).ToListAsync();
            return favoritos;
        }
    }
}