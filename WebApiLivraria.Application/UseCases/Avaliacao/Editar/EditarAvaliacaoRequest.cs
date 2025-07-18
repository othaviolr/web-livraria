namespace WebApiLivraria.Application.UseCases.Avaliacao.Editar
{
    public class EditarAvaliacaoRequest
    {
        public string Id { get; set; } = null!;
        public string UsuarioId { get; set; } = null!;
        public int Nota { get; set; }
        public string? Comentario { get; set; }
    }
}