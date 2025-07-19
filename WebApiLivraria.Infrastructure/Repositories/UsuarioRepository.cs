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
        private readonly IMongoCollection<Leitura> _leituras;

        public UsuarioRepository(MongoDbContext context)
        {
            _usuarios = context.Usuarios;
            _avaliacoes = context.Avaliacoes;
            _favoritos = context.Favoritos;
            _leituras = context.Leituras;
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

            usuario.DefinirAvaliacoes(avaliacoes);
            return usuario;
        }

        public async Task<Usuario?> ObterPorNomeUsuarioComRelacionamentosAsync(string nomeUsuario)
        {
            var filter = Builders<Usuario>.Filter.Eq(u => u.NomeUsuario, nomeUsuario);
            var usuario = await _usuarios.Find(filter).FirstOrDefaultAsync();

            if (usuario == null) return null;

            var filtroAval = Builders<Avaliacao>.Filter.Eq(a => a.UsuarioId, usuario.Id);
            var avaliacoes = await _avaliacoes.Find(filtroAval).ToListAsync();
            usuario.DefinirAvaliacoes(avaliacoes);

            var filtroFav = Builders<Favorito>.Filter.Eq(f => f.UsuarioId, usuario.Id);
            var favoritos = await _favoritos.Find(filtroFav).ToListAsync();
            usuario.DefinirFavoritos(favoritos);

            return usuario;
        }

        public async Task<Usuario?> ObterPorIdComDetalhesAsync(string id)
        {
            var usuario = await ObterPorId(id);
            if (usuario == null) return null;

            var filtroAval = Builders<Avaliacao>.Filter.Eq(a => a.UsuarioId, id);
            var avaliacoes = await _avaliacoes.Find(filtroAval).ToListAsync();
            usuario.DefinirAvaliacoes(avaliacoes);

            var filtroFav = Builders<Favorito>.Filter.Eq(f => f.UsuarioId, id);
            var favoritos = await _favoritos.Find(filtroFav).ToListAsync();
            usuario.DefinirFavoritos(favoritos);

            return usuario;
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