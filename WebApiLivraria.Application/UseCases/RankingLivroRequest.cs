namespace WebApiLivraria.Application.UseCases.RankingLivro;
public class RankingLivroRequest
{
    public int? Ano { get; set; }
    public string? Genero { get; set; }
    public int Pagina { get; set; } = 1;
    public int TamanhoPagina { get; set; } = 10;
}