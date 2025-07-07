using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.UseCases.Usuarios
{
    public class UsuarioPerfilDto
    {
        public string NomeUsuario { get; set; } = null!;
        public string? FotoUrl { get; set; }
        public string? Cidade { get; set; }
        public string Role { get; set; } = "Leitor";

        public List<AtividadeDto> Atividades { get; set; } = new List<AtividadeDto>();
    }
}