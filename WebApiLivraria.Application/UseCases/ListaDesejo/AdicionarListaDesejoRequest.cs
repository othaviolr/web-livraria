namespace WebApiLivraria.Application.UseCases.ListaDesejo
{
    public class AdicionarListaDesejoRequest
    {
        public int LivroId { get; set; }
        public Guid UsuarioId { get; set; }
    }
}