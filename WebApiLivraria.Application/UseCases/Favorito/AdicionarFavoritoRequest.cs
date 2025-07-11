namespace WebApiLivraria.Application.UseCases.Favorito;

public class AdicionarFavoritoRequest
{
    public int LivroId { get; set; }

    [System.Text.Json.Serialization.JsonIgnore]
    public Guid UsuarioId { get; set; }
}