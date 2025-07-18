namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico
{
    public class ResenhaDto
    {
        public string LivroId { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public string ImagemUrl { get; set; } = null!;
        public int Nota { get; set; }
        public string Comentario { get; set; } = null!;
        public DateTime Data { get; set; }
    }
}