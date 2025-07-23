using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico;
using WebApiLivraria.Domain.Enums;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilCompletoUseCase
{
    public class ObterPerfilCompletoUseCase
    {
        private readonly MongoDbContext _context;

        public ObterPerfilCompletoUseCase(MongoDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioPerfilDto> ExecutarAsync(string usuarioId)
        {
            var usuario = await _context.Usuarios.Find(u => u.Id == usuarioId).FirstOrDefaultAsync();
            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            var leituras = await _context.Leituras.Find(l => l.UsuarioId == usuarioId).ToListAsync();

            if (!leituras.Any())
            {
                return new UsuarioPerfilDto
                {
                    NomeUsuario = usuario.NomeUsuario ?? usuario.Nome,
                    FotoUrl = usuario.FotoUrl,
                    Cidade = usuario.Cidade,
                    Role = usuario.Role,
                    Bio = usuario.Bio,
                    QuantidadeFavoritos = 0,
                    QuantidadeQueroLer = 0,
                    QuantidadeLendo = 0,
                    QuantidadeLido = 0,
                    QuantidadeAbandonei = 0,
                    QuantidadeRelendo = 0,
                    LivrosMarcados = new List<LivroResumoDto>(),
                    Atividades = new List<AtividadeDto>()
                };
            }

            var livrosIds = leituras
                .Select(l => l.LivroId)
                .Distinct()
                .ToList();

            var livros = await _context.Livros
                .Find(l => livrosIds.Contains(l.Id))
                .ToListAsync();

            var autoresIds = livros
                .Where(l => !string.IsNullOrEmpty(l.AutorId))
                .Select(l => l.AutorId)
                .Distinct()
                .ToList();

            var autores = await _context.Autores
                .Find(a => autoresIds.Contains(a.Id))
                .ToListAsync();

            var livrosMarcados = leituras.Select(leitura =>
            {
                var livro = livros.FirstOrDefault(l => l.Id == leitura.LivroId);
                var autor = livro != null
                    ? autores.FirstOrDefault(a => a.Id == livro.AutorId)
                    : null;

                return new LivroResumoDto
                {
                    Id = leitura.LivroId,
                    Titulo = livro?.Titulo ?? "Desconhecido",
                    Autor = autor?.Nome ?? "Desconhecido",
                    ImagemUrl = livro?.ImagemUrl ?? string.Empty,
                    StatusLeitura = leitura.Status
                };
            }).ToList();

            var quantidadeFavoritos = (int)await _context.Favoritos.CountDocumentsAsync(f => f.UsuarioId == usuarioId);

            var contagemStatus = leituras
                .GroupBy(l => l.Status)
                .ToDictionary(g => g.Key, g => g.Count());

            var avaliacoes = await _context.Avaliacoes
                .Find(a => a.UsuarioId == usuarioId)
                .SortByDescending(a => a.DataCriacao)
                .Limit(10)
                .ToListAsync();

            var livrosAvaliacoesIds = avaliacoes.Select(a => a.LivroId).Distinct().ToList();

            var livrosAvaliacoes = await _context.Livros
                .Find(l => livrosAvaliacoesIds.Contains(l.Id))
                .ToListAsync();

            var atividades = avaliacoes.Select(a =>
            {
                var livro = livrosAvaliacoes.FirstOrDefault(l => l.Id == a.LivroId);
                return new AtividadeDto
                {
                    Tipo = "Avaliação",
                    Descricao = $"Avaliou o livro '{livro?.Titulo ?? "Livro desconhecido"}' com nota {a.Nota}",
                    Data = a.DataCriacao
                };
            }).ToList();

            return new UsuarioPerfilDto
            {
                NomeUsuario = usuario.NomeUsuario ?? usuario.Nome,
                FotoUrl = usuario.FotoUrl,
                Cidade = usuario.Cidade,
                Role = usuario.Role,
                Bio = usuario.Bio,

                QuantidadeFavoritos = quantidadeFavoritos,
                QuantidadeQueroLer = contagemStatus.TryGetValue(StatusLeitura.QueroLer, out var queroLer) ? queroLer : 0,
                QuantidadeLendo = contagemStatus.TryGetValue(StatusLeitura.Lendo, out var lendo) ? lendo : 0,
                QuantidadeLido = contagemStatus.TryGetValue(StatusLeitura.Lido, out var lido) ? lido : 0,
                QuantidadeAbandonei = contagemStatus.TryGetValue(StatusLeitura.Abandonei, out var abandonei) ? abandonei : 0,
                QuantidadeRelendo = contagemStatus.TryGetValue(StatusLeitura.Relendo, out var relendo) ? relendo : 0,

                LivrosMarcados = livrosMarcados,
                Atividades = atividades
            };
        }
    }
}