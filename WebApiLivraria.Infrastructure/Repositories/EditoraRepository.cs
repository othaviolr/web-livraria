using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class EditoraRepository : IEditoraRepository
    {
        private readonly IMongoCollection<Editora> _editoras;

        public EditoraRepository(MongoDbContext context)
        {
            _editoras = context.Editoras;
        }

        public async Task AdicionarAsync(Editora editora)
        {
            await _editoras.InsertOneAsync(editora);
        }

        public async Task AtualizarAsync(Editora editora)
        {
            await _editoras.ReplaceOneAsync(e => e.Id == editora.Id, editora);
        }

        public async Task RemoverAsync(string id)
        {
            await _editoras.DeleteOneAsync(e => e.Id == id);
        }

        public async Task<Editora?> ObterPorIdAsync(string id)
        {
            return await _editoras.Find(e => e.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Editora>> ListarAsync()
        {
            return await _editoras.Find(_ => true).ToListAsync();
        }
    }
}