using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico
{
    public class UsuarioPerfilPublicoDto
    {
        public string Nome { get; set; }
        public string NomeUsuario { get; set; }
        public string? FotoUrl { get; set; }
        public string? Bio { get; set; }
        public string? Cidade { get; set; }

        public List<LivroResumoDto> LivrosLidos { get; set; } = new();
        public List<LivroResumoDto> Favoritos { get; set; } = new();
        public List<LivroResumoDto> Wishlist { get; set; } = new();
        public List<ResenhaDto> ResenhasRecentes { get; set; } = new();

        public int TotalLido { get; set; }
        public int TotalLendo { get; set; }
        public int TotalQueroLer { get; set; }
        public int TotalRelendo { get; set; }
        public int TotalAbandonei { get; set; }
        public int TotalResenhas { get; set; }
        public List<UsuarioResumoDto> Seguidores { get; set; } = new();
        public List<UsuarioResumoDto> Seguindo { get; set; } = new();
    }
}