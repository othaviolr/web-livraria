namespace WebApiLivraria.Application.UseCases.Usuarios;

public class AtualizarPerfilRequest
{
    public string NomeUsuario { get; set; } = null!;
    public string? FotoUrl { get; set; }
    public string? Cidade { get; set; }
    public string Role { get; set; } = "Leitor";
}