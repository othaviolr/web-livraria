using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly IMongoCollection<Livro> _livros;

        public LivroRepository(MongoDbContext context)
        {
            _livros = context.Livros;
        }

        public async Task AdicionarAsync(Livro livro)
        {
            await _livros.InsertOneAsync(livro);
        }

        public async Task AtualizarAsync(Livro livro)
        {
            await _livros.ReplaceOneAsync(l => l.Id == livro.Id, livro);
        }

        public async Task RemoverAsync(string id)
        {
            await _livros.DeleteOneAsync(l => l.Id == id);
        }

        public async Task<Livro> ObterPorIdAsync(string id)
        {
            return await _livros.Find(l => l.Id == id).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Livro>> ListarAsync()
        {
            return await _livros.Find(_ => true).ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ListarComFiltroAsync(string? search)
        {
            if (string.IsNullOrWhiteSpace(search))
                return await ListarAsync();

            var regex = new MongoDB.Bson.BsonRegularExpression(search, "i");

            var filterTitulo = Builders<Livro>.Filter.Regex(l => l.Titulo, regex);
            var filterAutor = Builders<Livro>.Filter.Regex("Autor.Nome", regex);
            var filterEditora = Builders<Livro>.Filter.Regex("Editora.Nome", regex);

            var filter = Builders<Livro>.Filter.Or(filterTitulo, filterAutor, filterEditora);

            return await _livros.Find(filter).ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ObterLivrosComFiltroAsync(int? anoPublicacao, string? genero)
        {
            var builder = Builders<Livro>.Filter;
            var filter = builder.Empty;

            if (anoPublicacao.HasValue)
            {
                filter &= builder.Eq(l => l.AnoPublicacao.Year, anoPublicacao.Value);
            }

            if (!string.IsNullOrEmpty(genero))
            {
                filter &= builder.ElemMatch(l => l.LivroGeneros, lg => lg.Genero.Nome == genero);
            }

            return await _livros.Find(filter).ToListAsync();
        }
    }
}