using MongoDB.Driver;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Application.UseCases.Leitura.Listar
{
    public class ObterLeiturasUseCase
    {
        private readonly MongoDbContext _context;

        public ObterLeiturasUseCase(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<List<LivroMarcadoDto>> ExecutarAsync(string usuarioId)
        {
            var leituras = await _context.Leituras
                .Find(l => l.UsuarioId == usuarioId)
                .ToListAsync();

            var livroIds = leituras.Select(l => l.LivroId).ToList();

            var livros = await _context.Livros
                .Find(l => livroIds.Contains(l.Id))
                .ToListAsync();

            var livrosMarcados = leituras.Select(l =>
            {
                var livro = livros.FirstOrDefault(x => x.Id == l.LivroId);
                return new LivroMarcadoDto
                {
                    LivroId = l.LivroId,
                    Titulo = livro?.Titulo ?? "Desconhecido",
                    ImagemUrl = livro?.ImagemUrl ?? string.Empty,
                    Status = l.Status
                };
            }).ToList();

            return livrosMarcados;
        }
    }
}