using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLivraria.Application.Services;
using WebApiLivraria.Application.UseCases.RankingLivro;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Infrastructure.Contexts;

public class RankingService : IRankingService
{
    private readonly IMongoCollection<RankingLivro> _rankingCollection;
    private readonly IMongoCollection<Livro> _livrosCollection;
    private readonly IMongoCollection<Genero> _generosCollection;
    private readonly IMongoCollection<LivroGenero> _livroGenerosCollection;

    public RankingService(MongoDbContext context)
    {
        _rankingCollection = context.RankingLivros;
        _livrosCollection = context.Livros;
        _generosCollection = context.Generos;
        _livroGenerosCollection = context.LivroGeneros;
    }

    public async Task<List<RankingLivro>> ObterRankingGeralAsync(int pagina, int tamanhoPagina)
    {
        var filter = Builders<RankingLivro>.Filter.Eq(r => r.Genero, null);
        var sort = Builders<RankingLivro>.Sort.Ascending(r => r.Posicao);

        return await _rankingCollection
            .Find(filter)
            .Sort(sort)
            .Skip((pagina - 1) * tamanhoPagina)
            .Limit(tamanhoPagina)
            .ToListAsync();
    }

    public async Task<List<RankingLivro>> ObterRankingPorGeneroAsync(string genero, int pagina, int tamanhoPagina)
    {
        var filter = Builders<RankingLivro>.Filter.Eq(r => r.Genero, genero);
        var sort = Builders<RankingLivro>.Sort.Ascending(r => r.Posicao);

        return await _rankingCollection
            .Find(filter)
            .Sort(sort)
            .Skip((pagina - 1) * tamanhoPagina)
            .Limit(tamanhoPagina)
            .ToListAsync();
    }

    public async Task AtualizarRankingAsync()
    {
        // Remove todos os rankings atuais
        await _rankingCollection.DeleteManyAsync(Builders<RankingLivro>.Filter.Empty);

        // Livros que têm avaliações
        var livrosComAvaliacoes = await _livrosCollection
            .Find(l => l.Avaliacoes != null && l.Avaliacoes.Count > 0)
            .ToListAsync();

        // Ranking geral (sem gênero)
        var rankingGeral = livrosComAvaliacoes
            .Select(l => new RankingLivro
            {
                LivroId = l.Id, // l.Id é string
                Genero = null!,
                NotaMedia = l.Avaliacoes.Average(a => a.Nota),
                TotalAvaliacoes = l.Avaliacoes.Count,
                DataAtualizacao = DateTime.UtcNow
            })
            .OrderByDescending(r => r.NotaMedia)
            .ToList();

        for (int i = 0; i < rankingGeral.Count; i++)
            rankingGeral[i].Posicao = i + 1;

        // Ranking por gênero
        var generos = await _generosCollection.Find(_ => true).ToListAsync();
        var rankingPorGenero = new List<RankingLivro>();

        foreach (var genero in generos)
        {
            // Pegando os IDs de livros que pertencem a esse gênero
            var livroIds = await _livroGenerosCollection
                .Find(lg => lg.Genero.Nome == genero.Nome)
                .Project(lg => lg.Livro.Id) // Livro.Id é string
                .ToListAsync();

            // Livros com avaliações desse gênero
            var livrosGenero = await _livrosCollection
                .Find(l => livroIds.Contains(l.Id) && l.Avaliacoes != null && l.Avaliacoes.Count > 0)
                .ToListAsync();

            var rankingGenero = livrosGenero
                .Select(l => new RankingLivro
                {
                    LivroId = l.Id,
                    Genero = genero.Nome,
                    NotaMedia = l.Avaliacoes.Average(a => a.Nota),
                    TotalAvaliacoes = l.Avaliacoes.Count,
                    DataAtualizacao = DateTime.UtcNow
                })
                .OrderByDescending(r => r.NotaMedia)
                .ToList();

            for (int i = 0; i < rankingGenero.Count; i++)
                rankingGenero[i].Posicao = i + 1;

            rankingPorGenero.AddRange(rankingGenero);
        }

        // Inserir rankings no banco
        await _rankingCollection.InsertManyAsync(rankingGeral.Concat(rankingPorGenero));
    }

    public async Task<List<RankingLivroResponse>> ObterRankingAsync(RankingLivroRequest request)
    {
        List<RankingLivro> rankings;

        if (string.IsNullOrWhiteSpace(request.Genero))
            rankings = await ObterRankingGeralAsync(request.Pagina, request.TamanhoPagina);
        else
            rankings = await ObterRankingPorGeneroAsync(request.Genero, request.Pagina, request.TamanhoPagina);

        var livroIds = rankings.Select(r => r.LivroId).ToList();

        var livros = await _livrosCollection
            .Find(l => livroIds.Contains(l.Id))
            .ToListAsync();

        var resultado = rankings.Select(r =>
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
        }).ToList();

        return resultado;
    }
}