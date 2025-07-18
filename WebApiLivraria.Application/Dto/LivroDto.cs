namespace WebApiLivraria.Application.Dto
{
    public class LivroDto
    {
        public string Id { get; set; } = null!;

        public string Titulo { get; set; } = null!;

        public string AutorId { get; set; } = null!;

        public string? AutorNome { get; set; }

        public string EditoraId { get; set; } = null!;

        public string? EditoraNome { get; set; }

        public string? ImagemUrl { get; set; }

        public DateTime AnoPublicacao { get; set; }

        public List<string> Generos { get; set; } = new();

        public string? Sinopse { get; set; }

        public int NumeroPaginas { get; set; }

        public string? Idioma { get; set; }
    }
}