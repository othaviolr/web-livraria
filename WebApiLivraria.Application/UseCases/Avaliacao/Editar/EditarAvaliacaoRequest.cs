namespace WebApiLivraria.Application.UseCases.Avaliacao.Editar
{
    public class EditarAvaliacaoRequest
    {
        public int Id { get; set; }
        public Guid UsuarioId { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; }
    }
}