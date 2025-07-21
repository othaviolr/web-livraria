using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class ListaDesejoRepository : IListaDesejoRepository
    {
        private readonly IMongoCollection<ListaDesejo> _listasDesejo;

        public ListaDesejoRepository(MongoDbContext context)
        {
            _listasDesejo = context.ListasDesejo;
        }

        public async Task Adicionar(ListaDesejo listaDesejo)
        {
            await _listasDesejo.InsertOneAsync(listaDesejo);
        }

        public async Task Remover(string usuarioId, string livroId)
        {
            var usuarioIdStr = usuarioId.ToString();

            var filter = Builders<ListaDesejo>.Filter.And(
                Builders<ListaDesejo>.Filter.Eq(ld => ld.UsuarioId, usuarioIdStr),
                Builders<ListaDesejo>.Filter.Eq(ld => ld.LivroId, livroId)
            );

            await _listasDesejo.DeleteOneAsync(filter);
        }

        public async Task<bool> Existe(string usuarioId, string livroId)
        {
            var usuarioIdStr = usuarioId.ToString();

            var filter = Builders<ListaDesejo>.Filter.And(
                Builders<ListaDesejo>.Filter.Eq(ld => ld.UsuarioId, usuarioIdStr),
                Builders<ListaDesejo>.Filter.Eq(ld => ld.LivroId, livroId)
            );

            var count = await _listasDesejo.CountDocumentsAsync(filter);
            return count > 0;
        }

        public async Task<IReadOnlyCollection<ListaDesejo>> ListarPorUsuario(string usuarioId)
        {
            var usuarioIdStr = usuarioId.ToString();

            var filter = Builders<ListaDesejo>.Filter.Eq(ld => ld.UsuarioId, usuarioIdStr);
            return await _listasDesejo.Find(filter).ToListAsync();
        }
    }
}