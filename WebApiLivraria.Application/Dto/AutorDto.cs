namespace WebApiLivraria.Application.Dto
{
    public class AutorDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string? Biografia { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? LocalNascimento { get; set; }
        public string? FotoUrl { get; set; }
    }
}