namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico
{
    public class UsuarioPerfilPublicoDto
    {
        public string Nome { get; set; } = null!;
        public string NomeUsuario { get; set; } = null!;
        public string? FotoUrl { get; set; }
        public string? Bio { get; set; }

        public List<LivroResumoDto> LivrosLidos { get; set; } = new();
        public List<LivroResumoDto> Favoritos { get; set; } = new();
        public List<ResenhaDto> ResenhasRecentes { get; set; } = new();
    }
}