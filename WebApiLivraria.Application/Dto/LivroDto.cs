namespace WebApiLivraria.Application.Dto
{
    public class LivroDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public int AutorId { get; set; }
        public int EditoraId { get; set; }
        public List<int> Generos { get; set; } = new();
    }
}
