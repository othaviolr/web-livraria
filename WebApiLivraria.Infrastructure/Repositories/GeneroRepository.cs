using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly IMongoCollection<Genero> _generos;

        public GeneroRepository(MongoDbContext context)
        {
            _generos = context.Generos;
        }

        public async Task AdicionarAsync(Genero genero)
        {
            await _generos.InsertOneAsync(genero);
        }

        public async Task AtualizarAsync(Genero genero)
        {
            await _generos.ReplaceOneAsync(g => g.Id == genero.Id, genero);
        }

        public async Task RemoverAsync(string id)
        {
            await _generos.DeleteOneAsync(g => g.Id == id);
        }

        public async Task<Genero?> ObterPorIdAsync(string id)
        {
            return await _generos.Find(g => g.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Genero>> ListarAsync()
        {
            return await _generos.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Genero>> ListarPorIdsAsync(IEnumerable<string> ids)
        {
            var filter = Builders<Genero>.Filter.In(g => g.Id, ids);
            return await _generos.Find(filter).ToListAsync();
        }
    }
}