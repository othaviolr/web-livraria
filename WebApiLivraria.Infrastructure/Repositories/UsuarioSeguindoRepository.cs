using MongoDB.Driver;
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

        public async Task DeixarDeSeguirAsync(string seguidorId, string seguindoId)
        {
            var filter = Builders<UsuarioSeguindo>.Filter.And(
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguidorId, seguidorId),
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguindoId, seguindoId)
            );

            await _usuariosSeguindo.DeleteOneAsync(filter);
        }

        public async Task<bool> ExisteRelacionamentoAsync(string seguidorId, string seguindoId)
        {
            var filter = Builders<UsuarioSeguindo>.Filter.And(
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguidorId, seguidorId),
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguindoId, seguindoId)
            );

            return await _usuariosSeguindo.Find(filter).AnyAsync();
        }

        public async Task<List<string>> ObterSeguidoresIdsAsync(string usuarioId)
        {
            var filter = Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguindoId, usuarioId);
            var seguidores = await _usuariosSeguindo.Find(filter).ToListAsync();
            return seguidores.Select(s => s.SeguidorId).ToList();
        }

        public async Task<List<string>> ObterSeguindoIdsAsync(string usuarioId)
        {
            var filter = Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguidorId, usuarioId);
            var seguindo = await _usuariosSeguindo.Find(filter).ToListAsync();
            return seguindo.Select(s => s.SeguindoId).ToList();
        }

        public async Task<List<Usuario>> ObterSeguidoresAsync(string usuarioId)
        {
            var seguidoresIds = await ObterSeguidoresIdsAsync(usuarioId);
            if (!seguidoresIds.Any())
                return new List<Usuario>();

            var filterUsuarios = Builders<Usuario>.Filter.In(u => u.Id, seguidoresIds);
            return await _usuarios.Find(filterUsuarios).ToListAsync();
        }

        public async Task<List<Usuario>> ObterSeguindoAsync(string usuarioId)
        {
            var seguindoIds = await ObterSeguindoIdsAsync(usuarioId);
            if (!seguindoIds.Any())
                return new List<Usuario>();

            var filterUsuarios = Builders<Usuario>.Filter.In(u => u.Id, seguindoIds);
            return await _usuarios.Find(filterUsuarios).ToListAsync();
        }

        public async Task<bool> VerificarSeSegueAsync(string seguidorId, string seguindoId)
        {
            var filtro = Builders<UsuarioSeguindo>.Filter.And(
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguidorId, seguidorId),
                Builders<UsuarioSeguindo>.Filter.Eq(us => us.SeguindoId, seguindoId)
            );

            return await _usuariosSeguindo.Find(filtro).AnyAsync();
        }
    }
}