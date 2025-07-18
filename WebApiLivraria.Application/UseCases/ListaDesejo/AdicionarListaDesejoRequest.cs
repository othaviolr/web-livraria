namespace WebApiLivraria.Application.UseCases.ListaDesejo
{
    public class AdicionarListaDesejoRequest
    {
        public Guid UsuarioId { get; set; }
        public string LivroId { get; set; }
    }
}