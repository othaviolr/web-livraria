namespace WebApiLivraria.Application.UseCases.Favorito;

public class AdicionarFavoritoRequest
{
    public string UsuarioId { get; set; } = string.Empty;
    public string LivroId { get; set; } = string.Empty;
}