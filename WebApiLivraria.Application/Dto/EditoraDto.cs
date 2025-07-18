namespace WebApiLivraria.Application.Dto
{
    public class EditoraDto
    {
        public string Id { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string? Biografia { get; set; }
        public string? ImagemUrl { get; set; }
    }
}