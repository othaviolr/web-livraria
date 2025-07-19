using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico;

namespace WebApiLivraria.Application.UseCases.Usuarios
{
    public class UsuarioPerfilDto
    {
        public string Id { get; set; } = null!;          
        public string NomeUsuario { get; set; } = null!;
        public string? FotoUrl { get; set; }
        public string? Cidade { get; set; }
        public string Role { get; set; } = "Leitor";
        public string? Bio { get; set; }

        public int QuantidadeFavoritos { get; set; }
        public int QuantidadeQueroLer { get; set; }
        public int QuantidadeLendo { get; set; }
        public int QuantidadeLido { get; set; }
        public int QuantidadeAbandonei { get; set; }
        public int QuantidadeRelendo { get; set; }

        public List<LivroResumoDto> LivrosMarcados { get; set; } = new List<LivroResumoDto>();

        public List<AtividadeDto> Atividades { get; set; } = new List<AtividadeDto>();
    }
}