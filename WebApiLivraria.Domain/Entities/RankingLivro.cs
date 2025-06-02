namespace WebApiLivraria.Domain.Entities
{
    public class RankingLivro
    {
        public int Id { get; set; }
        public string Genero { get; set; }
        public int Posicao { get; set; }
        public int LivroId { get; set; }
        public Livro Livro { get; set; }
        public double NotaMedia { get; set; }
        public int TotalAvaliacoes { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}
