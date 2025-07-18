using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class UsuarioSeguindoRepository : IUsuarioSeguindoRepository
    {
        private readonly IMongoCollection<UsuarioSeguindo> _usuariosSeguindo;
        private readonly IMongoCollection<Usuario> _usuarios;

        public UsuarioSeguindoRepository(MongoDbContext context)
        {
            _usuariosSeguindo = context.UsuariosSeguindo;
            _usuarios = context.Usuarios;
        }

        public async Task SeguirAsync(UsuarioSeguindo usuarioSeguindo)
        {
            await _usuariosSeguindo.InsertOneAsync(usuarioSeguindo);
        }

        public async Task DeixarDeSeguirAsync(Guid seguidorId, Guid seguindoId)
        {
            string seguidorIdStr = seguidorId.ToString();
            string seguindoIdStr = seguindoId.ToString();

            var filter = Builders<UsuarioSeguindo>.Filter.And(
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguidorId, seguidorIdStr),
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguindoId, seguindoIdStr)
            );

            await _usuariosSeguindo.DeleteOneAsync(filter);
        }

        public async Task<bool> ExisteRelacionamentoAsync(Guid seguidorId, Guid seguindoId)
        {
            string seguidorIdStr = seguidorId.ToString();
            string seguindoIdStr = seguindoId.ToString();

            var filter = Builders<UsuarioSeguindo>.Filter.And(
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguidorId, seguidorIdStr),
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguindoId, seguindoIdStr)
            );

            return await _usuariosSeguindo.Find(filter).AnyAsync();
        }

        public async Task<List<string>> ObterSeguidoresIdsAsync(Guid usuarioId)
        {
            string usuarioIdStr = usuarioId.ToString();
            var filter = Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguindoId, usuarioIdStr);
            var seguidores = await _usuariosSeguindo.Find(filter).ToListAsync();
            return seguidores.Select(s => s.SeguidorId).ToList();
        }

        public async Task<List<string>> ObterSeguindoIdsAsync(Guid usuarioId)
        {
            string usuarioIdStr = usuarioId.ToString();
            var filter = Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguidorId, usuarioIdStr);
            var seguindo = await _usuariosSeguindo.Find(filter).ToListAsync();
            return seguindo.Select(s => s.SeguindoId).ToList();
        }

        public async Task<List<Usuario>> ObterSeguidoresAsync(Guid usuarioId)
        {
            var seguidoresIds = await ObterSeguidoresIdsAsync(usuarioId);
            if (!seguidoresIds.Any())
                return new List<Usuario>();

            var filterUsuarios = Builders<Usuario>.Filter.In(u => u.Id, seguidoresIds);
            return await _usuarios.Find(filterUsuarios).ToListAsync();
        }

        public async Task<List<Usuario>> ObterSeguindoAsync(Guid usuarioId)
        {
            var seguindoIds = await ObterSeguindoIdsAsync(usuarioId);
            if (!seguindoIds.Any())
                return new List<Usuario>();

            var filterUsuarios = Builders<Usuario>.Filter.In(u => u.Id, seguindoIds);
            return await _usuarios.Find(filterUsuarios).ToListAsync();
        }
    }
}