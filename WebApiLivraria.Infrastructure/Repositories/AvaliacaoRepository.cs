using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Contexts;
using System.Linq;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly IMongoCollection<Avaliacao> _avaliacoes;
        private readonly IMongoCollection<Usuario> _usuarios;
        private readonly IMongoCollection<Livro> _livros;

        public AvaliacaoRepository(MongoDbContext context)
        {
            _avaliacoes = context.Avaliacoes;
            _usuarios = context.Usuarios;
            _livros = context.Livros;
        }

        public async Task<IEnumerable<Avaliacao>> ListarPorLivroIdAsync(string livroId)
        {
            var filtro = Builders<Avaliacao>.Filter.Eq(a => a.LivroId, livroId);
            var avaliacoes = await _avaliacoes.Find(filtro).ToListAsync();

            foreach (var a in avaliacoes)
            {
                var usuario = await _usuarios.Find(u => u.Id == a.UsuarioId).FirstOrDefaultAsync();
                if (usuario != null)
                {
                    a.DefinirUsuario(usuario);
                }
            }

            return avaliacoes;
        }

        public async Task<IEnumerable<Avaliacao>> ListarPorUsuarioIdAsync(string usuarioId)
        {
            var filtro = Builders<Avaliacao>.Filter.Eq(a => a.UsuarioId, usuarioId);
            var avaliacoes = await _avaliacoes.Find(filtro).ToListAsync();

            foreach (var a in avaliacoes)
            {
                var livro = await _livros.Find(l => l.Id == a.LivroId).FirstOrDefaultAsync();
                if (livro != null)
                {
                    a.DefinirLivro(livro);
                }
            }

            return avaliacoes;
        }

        public async Task AdicionarAsync(Avaliacao avaliacao)
        {
            await _avaliacoes.InsertOneAsync(avaliacao);
        }

        public async Task AtualizarAsync(Avaliacao avaliacao)
        {
            await _avaliacoes.ReplaceOneAsync(a => a.Id == avaliacao.Id, avaliacao);
        }

        public async Task RemoverAsync(string id)
        {
            await _avaliacoes.DeleteOneAsync(a => a.Id == id);
        }

        public async Task<double> ObterMediaNotasPorLivroAsync(string livroId)
        {
            var filtro = Builders<Avaliacao>.Filter.Eq(a => a.LivroId, livroId);
            var avaliacoes = await _avaliacoes.Find(filtro).ToListAsync();

            return avaliacoes.Any() ? avaliacoes.Average(a => a.Nota) : 0.0;
        }

        public async Task<int> ObterQuantidadeAvaliacoesPorLivroAsync(string livroId)
        {
            var filtro = Builders<Avaliacao>.Filter.Eq(a => a.LivroId, livroId);
            return (int)await _avaliacoes.CountDocumentsAsync(filtro);
        }

        public async Task<Avaliacao?> ObterPorIdAsync(string id)
        {
            return await _avaliacoes.Find(a => a.Id == id).FirstOrDefaultAsync();
        }
    }
}