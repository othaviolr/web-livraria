namespace WebApiLivraria.Application.UseCases.ListaDesejo
{
    public class AdicionarListaDesejoRequest
    {
        public int LivroId { get; set; }

        [System.Text.Json.Serialization.JsonIgnore]
        public Guid UsuarioId { get; set; }
    }
}