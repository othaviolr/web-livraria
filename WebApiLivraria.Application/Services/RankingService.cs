using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Application.Services;
using WebApiLivraria.Application.UseCases.RankingLivro;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Infrastructure.Context;

public class RankingService : IRankingService
{
    private readonly AppDbContext _context;

    public RankingService(AppDbContext context)
    {
        _context = context;
    }

    public async Task<List<RankingLivro>> ObterRankingGeralAsync(int pagina, int tamanhoPagina)
    {
        return await _context.RankingLivros
            .Where(r => r.Genero == null)
            .OrderBy(r => r.Posicao)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .Include(r => r.Livro)
            .ToListAsync();
    }

    public async Task<List<RankingLivro>> ObterRankingPorGeneroAsync(string genero, int pagina, int tamanhoPagina)
    {
        return await _context.RankingLivros
            .Where(r => r.Genero != null && r.Genero.ToLower() == genero.ToLower())
            .OrderBy(r => r.Posicao)
            .Skip((pagina - 1) * tamanhoPagina)
            .Take(tamanhoPagina)
            .Include(r => r.Livro)
            .ToListAsync();
    }

    public async Task AtualizarRankingAsync()
    {
        var todosRankings = await _context.RankingLivros.ToListAsync();
        _context.RankingLivros.RemoveRange(todosRankings);
        await _context.SaveChangesAsync();

        var rankingGeral = await _context.Livros
            .Include(l => l.Avaliacoes)
            .Where(l => l.Avaliacoes.Any())
            .Select(l => new RankingLivro
            {
                LivroId = l.Id,
                Genero = null,
                NotaMedia = l.Avaliacoes.Average(a => a.Nota),
                TotalAvaliacoes = l.Avaliacoes.Count,
                DataAtualizacao = DateTime.UtcNow
            })
            .OrderByDescending(r => r.NotaMedia)
            .ToListAsync();

        for (int i = 0; i < rankingGeral.Count; i++)
        {
            rankingGeral[i].Posicao = i + 1;
        }

        var generos = await _context.Generos.Select(g => g.Nome).ToListAsync();
        var rankingPorGenero = new List<RankingLivro>();

        foreach (var genero in generos)
        {
            var livrosGenero = await _context.LivroGeneros
                .Where(lg => lg.Genero.Nome == genero)
                .Select(lg => lg.Livro)
                .Include(l => l.Avaliacoes)
                .Where(l => l.Avaliacoes.Any())
                .ToListAsync();

            var rankingGenero = livrosGenero
                .Select(l => new RankingLivro
                {
                    LivroId = l.Id,
                    Genero = genero,
                    NotaMedia = l.Avaliacoes.Average(a => a.Nota),
                    TotalAvaliacoes = l.Avaliacoes.Count,
                    DataAtualizacao = DateTime.UtcNow
                })
                .OrderByDescending(r => r.NotaMedia)
                .ToList();

            for (int i = 0; i < rankingGenero.Count; i++)
            {
                rankingGenero[i].Posicao = i + 1;
            }

            rankingPorGenero.AddRange(rankingGenero);
        }

        await _context.RankingLivros.AddRangeAsync(rankingGeral);
        await _context.RankingLivros.AddRangeAsync(rankingPorGenero);
        await _context.SaveChangesAsync();
    }

    public async Task<List<RankingLivroResponse>> ObterRankingAsync(RankingLivroRequest request)
    {
        List<RankingLivro> rankings;

        if (string.IsNullOrWhiteSpace(request.Genero))
        {
            rankings = await ObterRankingGeralAsync(request.Pagina, request.TamanhoPagina);
        }
        else
        {
            rankings = await ObterRankingPorGeneroAsync(request.Genero, request.Pagina, request.TamanhoPagina);
        }

        var livros = await _context.Livros
            .Where(l => rankings.Select(r => r.LivroId).Contains(l.Id))
            .Include(l => l.Autor)
            .ToListAsync();

        var resultado = rankings
            .Select(r =>
            {
                var livro = livros.FirstOrDefault(l => l.Id == r.LivroId);
                return new RankingLivroResponse
                {
                    LivroId = r.LivroId,
                    Titulo = livro?.Titulo ?? "Desconhecido",
                    Autor = livro?.Autor?.Nome ?? "Desconhecido",
                    NotaMedia = r.NotaMedia,
                    Posicao = r.Posicao,
                    TotalAvaliacoes = r.TotalAvaliacoes,
                    Genero = r.Genero,
                    DataAtualizacao = r.DataAtualizacao
                };
            })
            .ToList();

        return resultado;
    }
}
