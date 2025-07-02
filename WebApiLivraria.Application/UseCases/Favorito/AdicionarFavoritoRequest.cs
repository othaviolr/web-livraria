namespace WebApiLivraria.Application.UseCases.Favorito;

public class AdicionarFavoritoRequest
{
    public int LivroId { get; set; }
    public Guid UsuarioId { get; set; }
}