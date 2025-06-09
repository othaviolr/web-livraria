namespace WebApiLivraria.Application.UseCases.Favorito;

public class AdicionarFavoritoRequest
{
    public Guid LivroId { get; set; }
    public Guid UsuarioId { get; set; }
}