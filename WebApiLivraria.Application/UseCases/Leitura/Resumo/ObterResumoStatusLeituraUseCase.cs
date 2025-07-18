using MongoDB.Driver;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Domain.Enums;
using WebApiLivraria.Infrastructure.Contexts;
using EntAvaliacao = WebApiLivraria.Domain.Entities.Avaliacao;
using EntLeitura = WebApiLivraria.Domain.Entities.Leitura;

namespace WebApiLivraria.Application.UseCases.Leitura.Resumo
{
    public class ObterResumoStatusLeituraUseCase
    {
        private readonly IMongoCollection<EntLeitura> _leiturasCollection;
        private readonly IMongoCollection<EntAvaliacao> _avaliacoesCollection;

        public ObterResumoStatusLeituraUseCase(MongoDbContext context)
        {
            _leiturasCollection = context.Leituras;
            _avaliacoesCollection = context.Avaliacoes;
        }

        public async Task<ResumoStatusLeituraDto> ExecutarAsync(string usuarioId)
        {
            var filtroLeituras = Builders<EntLeitura>.Filter.Eq(l => l.UsuarioId, usuarioId);
            var leituras = await _leiturasCollection.Find(filtroLeituras).ToListAsync();

            var filtroAvaliacoes = Builders<EntAvaliacao>.Filter.Eq(a => a.UsuarioId, usuarioId);
            var resenhasCount = await _avaliacoesCollection.CountDocumentsAsync(filtroAvaliacoes);

            var resumo = new ResumoStatusLeituraDto
            {
                QueroLer = leituras.Count(l => l.Status == StatusLeitura.QueroLer),
                Lendo = leituras.Count(l => l.Status == StatusLeitura.Lendo),
                Lidos = leituras.Count(l => l.Status == StatusLeitura.Lido),
                Abandonei = leituras.Count(l => l.Status == StatusLeitura.Abandonei),
                Relendo = leituras.Count(l => l.Status == StatusLeitura.Relendo),
                Resenhas = (int)resenhasCount
            };

            return resumo;
        }
    }
}