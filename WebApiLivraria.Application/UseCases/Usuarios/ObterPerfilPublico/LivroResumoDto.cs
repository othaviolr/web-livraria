using WebApiLivraria.Domain.Enums;

namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico
{
    public class LivroResumoDto
    {
        public string Id { get; set; }
        public string Titulo { get; set; }
        public string Autor { get; set; }
        public string? ImagemUrl { get; set; }
        public StatusLeitura StatusLeitura { get; set; }
    }
}