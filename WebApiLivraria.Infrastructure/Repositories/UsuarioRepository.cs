using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Enums;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly IMongoCollection<Usuario> _usuarios;
        private readonly IMongoCollection<Avaliacao> _avaliacoes;
        private readonly IMongoCollection<Favorito> _favoritos;
        private readonly IMongoCollection<ListaDesejo> _listasDesejo; // <-- ADICIONADO
        private readonly IMongoCollection<Leitura> _leituras;
        private readonly IMongoCollection<Livro> _livros;
        private readonly IMongoCollection<Autor> _autores;

        private readonly IUsuarioSeguindoRepository _usuarioSeguindoRepository;

        public UsuarioRepository(MongoDbContext context, IUsuarioSeguindoRepository usuarioSeguindoRepository)
        {
            _usuarios = context.Usuarios;
            _avaliacoes = context.Avaliacoes;
            _favoritos = context.Favoritos;
            _listasDesejo = context.ListasDesejo; // <-- INICIALIZADO
            _leituras = context.Leituras;
            _livros = context.Livros;
            _autores = context.Autores;
            _usuarioSeguindoRepository = usuarioSeguindoRepository;
        }

        public async Task<Usuario?> ObterPorEmail(string email)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Email, email);
            return await _usuarios.Find(filter).FirstOrDefaultAsync();
        }

        public async Task<Usuario?> ObterPorId(string id)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Id, id);
            return await _usuarios.Find(filter).FirstOrDefaultAsync();
        }

        public async Task Adicionar(Usuario usuario)
        {
            await _usuarios.InsertOneAsync(usuario);
        }

        public async Task Atualizar(Usuario usuario)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Id, usuario.Id);
            await _usuarios.ReplaceOneAsync(filter, usuario);
        }

        public async Task Remover(Usuario usuario)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Id, usuario.Id);
            await _usuarios.DeleteOneAsync(filter);
        }

        public async Task<Usuario?> ObterPorIdComAvaliacoesAsync(string id)
        {
            var usuario = await ObterPorId(id);
            if (usuario == null) return null;

            var filtroAval = Builders<Avaliacao>.Filter.Eq(a => a.UsuarioId, id);
            var avaliacoes = await _avaliacoes.Find(filtroAval).ToListAsync();

            foreach (var avaliacao in avaliacoes)
            {
                var livro = await BuscarLivroCompletoPorIdAsync(avaliacao.LivroId);
                if (livro != null)
                    avaliacao.DefinirLivro(livro);
            }

            usuario.DefinirAvaliacoes(avaliacoes);
            return usuario;
        }

        public async Task<Usuario?> ObterPorNomeUsuarioComRelacionamentosAsync(string nomeUsuario)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.NomeUsuario, nomeUsuario);
            var usuario = await _usuarios.Find(filter).FirstOrDefaultAsync();

            if (usuario == null) return null;

            // Avaliações
            var filtroAval = Builders<Avaliacao>.Filter.Eq(a => a.UsuarioId, usuario.Id);
            var avaliacoes = await _avaliacoes.Find(filtroAval).ToListAsync();
            foreach (var avaliacao in avaliacoes)
            {
                var livro = await BuscarLivroCompletoPorIdAsync(avaliacao.LivroId);
                if (livro != null)
                    avaliacao.DefinirLivro(livro);
            }
            usuario.DefinirAvaliacoes(avaliacoes);

            var filtroFav = Builders<Favorito>.Filter.Eq(f => f.UsuarioId, usuario.Id);
            var favoritos = await _favoritos.Find(filtroFav).ToListAsync();
            foreach (var favorito in favoritos)
            {
                var livro = await BuscarLivroCompletoPorIdAsync(favorito.LivroId);
                if (livro != null)
                    favorito.DefinirLivro(livro);
            }
            usuario.DefinirFavoritos(favoritos);

            var filtroListaDesejo = Builders<ListaDesejo>.Filter.Eq(l => l.UsuarioId, usuario.Id);
            var listasDesejo = await _listasDesejo.Find(filtroListaDesejo).ToListAsync();
            foreach (var item in listasDesejo)
            {
                var livro = await BuscarLivroCompletoPorIdAsync(item.LivroId);
                if (livro != null)
                    item.DefinirLivro(livro);
            }
            usuario.DefinirListasDesejo(listasDesejo);

            var seguidoresUsuarios = await _usuarioSeguindoRepository.ObterSeguidoresAsync(usuario.Id);
            var seguidoresRelacionamentos = seguidoresUsuarios.Select(s =>
            {
                var rel = new UsuarioSeguindo(s.Id, usuario.Id);
                rel.DefinirSeguidor(s);
                return rel;
            }).ToList();
            usuario.DefinirSeguidores(seguidoresRelacionamentos);

            var seguindoUsuarios = await _usuarioSeguindoRepository.ObterSeguindoAsync(usuario.Id);
            var seguindoRelacionamentos = seguindoUsuarios.Select(s =>
            {
                var rel = new UsuarioSeguindo(usuario.Id, s.Id);
                rel.DefinirSeguindo(s);
                return rel;
            }).ToList();
            usuario.DefinirSeguindo(seguindoRelacionamentos);

            return usuario;
        }

        public async Task<Usuario?> ObterPorIdComDetalhesAsync(string id)
        {
            var usuario = await ObterPorId(id);
            if (usuario == null) return null;

            var filtroAval = Builders<Avaliacao>.Filter.Eq(a => a.UsuarioId, id);
            var avaliacoes = await _avaliacoes.Find(filtroAval).ToListAsync();
            foreach (var avaliacao in avaliacoes)
            {
                var livro = await BuscarLivroCompletoPorIdAsync(avaliacao.LivroId);
                if (livro != null)
                    avaliacao.DefinirLivro(livro);
            }
            usuario.DefinirAvaliacoes(avaliacoes);

            var filtroFav = Builders<Favorito>.Filter.Eq(f => f.UsuarioId, id);
            var favoritos = await _favoritos.Find(filtroFav).ToListAsync();
            foreach (var favorito in favoritos)
            {
                var livro = await BuscarLivroCompletoPorIdAsync(favorito.LivroId);
                if (livro != null)
                    favorito.DefinirLivro(livro);
            }
            usuario.DefinirFavoritos(favoritos);

            return usuario;
        }

        private async Task<Livro?> BuscarLivroCompletoPorIdAsync(string livroId)
        {
            if (string.IsNullOrEmpty(livroId))
                return null;

            var livro = await _livros.Find(l => l.Id == livroId).FirstOrDefaultAsync();
            if (livro == null)
                return null;

            var autor = await _autores.Find(a => a.Id == livro.AutorId).FirstOrDefaultAsync();
            if (autor != null)
                livro.DefinirAutor(autor);

            return livro;
        }

        public async Task<Dictionary<StatusLeitura, int>> ObterContagemLivrosPorStatusAsync(string usuarioId)
        {
            var filtro = Builders<Leitura>.Filter.Eq(l => l.UsuarioId, usuarioId);
            var agrupado = await _leituras.Aggregate()
                .Match(filtro)
                .Group(l => l.Status, g => new { Status = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            var resultado = Enum.GetValues(typeof(StatusLeitura))
                .Cast<StatusLeitura>()
                .ToDictionary(status => status, status => 0);

            foreach (var item in agrupado)
            {
                resultado[item.Status] = item.Quantidade;
            }

            return resultado;
        }

        public async Task<int> ObterQuantidadeFavoritosAsync(string usuarioId)
        {
            var filtro = Builders<Favorito>.Filter.Eq(f => f.UsuarioId, usuarioId);
            return (int)await _favoritos.CountDocumentsAsync(filtro);
        }

        public async Task<bool> ExistePorIdAsync(string id)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.Id, id);
            return await _usuarios.Find(filter).AnyAsync();
        }
    }
}