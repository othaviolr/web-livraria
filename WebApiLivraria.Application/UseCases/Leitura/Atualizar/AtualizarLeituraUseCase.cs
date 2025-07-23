using MongoDB.Driver;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Infrastructure.Contexts;
using EntLeitura = WebApiLivraria.Domain.Entities.Leitura;

namespace WebApiLivraria.Application.UseCases.Leitura.Atualizar
{
    public class AtualizarLeituraUseCase
    {
        private readonly IMongoCollection<EntLeitura> _leiturasCollection;

        public AtualizarLeituraUseCase(MongoDbContext context)
        {
            _leiturasCollection = context.Leituras;
        }

        public async Task<EntLeitura> ExecutarAsync(string usuarioId, AtualizarLeituraDto dto)
        {
            var filtroUsuario = Builders<EntLeitura>.Filter.Eq(l => l.UsuarioId, usuarioId);
            var filtroLivro = Builders<EntLeitura>.Filter.Eq(l => l.LivroId, dto.LivroId);
            var filtro = Builders<EntLeitura>.Filter.And(filtroUsuario, filtroLivro);

            var leituraExistente = await _leiturasCollection.Find(filtro).FirstOrDefaultAsync();

            if (leituraExistente == null)
            {
                var novaLeitura = new EntLeitura(usuarioId, dto.LivroId, dto.Status);
                await _leiturasCollection.InsertOneAsync(novaLeitura);
                return novaLeitura;
            }
            else
            {
                leituraExistente.AtualizarStatus(dto.Status);

                var update = Builders<EntLeitura>.Update.Set(l => l.Status, dto.Status);

                await _leiturasCollection.UpdateOneAsync(filtro, update);

                return leituraExistente;
            }
        }
    }
}