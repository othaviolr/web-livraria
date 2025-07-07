namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico
{
    public class LivroResumoDto
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public string? ImagemUrl { get; set; }
    }
}