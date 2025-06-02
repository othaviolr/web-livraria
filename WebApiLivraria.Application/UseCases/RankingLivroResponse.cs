namespace WebApiLivraria.Application.UseCases.RankingLivro;

public class RankingLivroResponse
{
    public int LivroId { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Autor { get; set; } = string.Empty;
    public double NotaMedia { get; set; }
    public int Posicao { get; set; }
    public int TotalAvaliacoes { get; set; }
    public string? Genero { get; set; }
    public DateTime DataAtualizacao { get; set; }
}
