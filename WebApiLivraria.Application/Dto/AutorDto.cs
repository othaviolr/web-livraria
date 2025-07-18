namespace WebApiLivraria.Application.Dto
{
    public class AutorDto
    {
        public string Id { get; set; } = null!; 
        public string Nome { get; set; } = null!;
        public string? EditoraId { get; set; } 
        public string? Biografia { get; set; }
        public DateTime? DataNascimento { get; set; }
        public string? LocalNascimento { get; set; }
        public string? FotoUrl { get; set; }
    }
}