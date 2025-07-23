using System.Threading.Tasks;
using MongoDB.Driver;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Application.UseCases.Leitura.Remover
{
    public class RemoverLeituraUseCase
    {
        private readonly MongoDbContext _context;

        public RemoverLeituraUseCase(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ExecutarAsync(string usuarioId, string livroId)
        {
            var filtro = Builders<Domain.Entities.Leitura>.Filter.And(
                Builders<Domain.Entities.Leitura>.Filter.Eq(l => l.UsuarioId, usuarioId),
                Builders<Domain.Entities.Leitura>.Filter.Eq(l => l.LivroId, livroId)
            );

            var resultado = await _context.Leituras.DeleteOneAsync(filtro);

            return resultado.DeletedCount > 0;
        }
    }
}