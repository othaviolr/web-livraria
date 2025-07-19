using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly IMongoCollection<Autor> _autores;

        public AutorRepository(MongoDbContext context)
        {
            _autores = context.Autores;
        }

        public async Task<IEnumerable<Autor>> ListarAsync(string filtro = null, string? editoraId = null)
        {
            var filterBuilder = Builders<Autor>.Filter;
            var filter = filterBuilder.Empty;

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroLower = filtro.ToLower();
                filter &= filterBuilder.Regex(a => a.Nome, new MongoDB.Bson.BsonRegularExpression(filtroLower, "i"));
            }

            if (!string.IsNullOrWhiteSpace(editoraId))
            {
                filter &= filterBuilder.Eq(a => a.EditoraId, editoraId);
            }

            return await _autores.Find(filter).ToListAsync();
        }

        public async Task<Autor?> ObterPorIdAsync(string id)
        {
            return await _autores.Find(a => a.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Autor> AdicionarAsync(Autor autor)
        {
            await _autores.InsertOneAsync(autor);
            return autor;
        }

        public async Task<bool> AtualizarAsync(Autor autor)
        {
            var result = await _autores.ReplaceOneAsync(a => a.Id == autor.Id, autor);
            return result.ModifiedCount > 0;
        }

        public async Task RemoverAsync(string id)
        {
            await _autores.DeleteOneAsync(a => a.Id == id);
        }
    }
}