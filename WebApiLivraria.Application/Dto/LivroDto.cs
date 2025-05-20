namespace WebApiLivraria.Application.Dto
{
    public class LivroDto
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public Guid AutorId { get; set; }
        public Guid EditoraId { get; set; }
        public List<Guid> Generos { get; set; } = new();
    }
}
