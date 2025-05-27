namespace WebApiLivraria.Application.UseCases.RankingLivro;

public class RankingLivroResponse
{
    public int Posicao { get; set; }
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Autor { get; set; }
    public string Genero { get; set; }
    public int AnoPublicacao { get; set; }
    public double NotaMedia { get; set; }
}
