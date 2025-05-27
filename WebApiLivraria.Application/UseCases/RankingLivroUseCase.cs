using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Application.UseCases.RankingLivro;

namespace WebApiLivraria.Application.UseCases.RankingLivro;

public class RankingLivroUseCase : IRankingLivroUseCase
{
    private readonly ILivroRepository _livroRepository;

    public RankingLivroUseCase(ILivroRepository livroRepository)
    {
        _livroRepository = livroRepository;
    }

    public async Task<List<RankingLivroResponse>> ExecutarAsync(RankingLivroRequest request)
    {
        var livros = await _livroRepository.ObterLivrosComFiltroAsync(
            request.Ano,
            request.Genero
        );

        var livrosOrdenados = livros
            .OrderByDescending(l => l.NotaMedia)
            .ToList();

        var livrosPaginados = livrosOrdenados
            .Skip((request.Pagina - 1) * request.TamanhoPagina)
            .Take(request.TamanhoPagina)
            .ToList();

        return livrosPaginados.Select((livro, index) => new RankingLivroResponse
        {
            Posicao = ((request.Pagina - 1) * request.TamanhoPagina) + index + 1,
            Id = livro.Id,
            Titulo = livro.Titulo,
            Autor = livro.Autor.Nome,
            Genero = livro.Genero,
            AnoPublicacao = livro.AnoPublicacao,
            NotaMedia = livro.NotaMedia
        }).ToList();
    }
}
