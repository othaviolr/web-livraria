namespace WebApiLivraria.Application.UseCases.Avaliacao.Criar
{
    public class CriarAvaliacaoRequest
    {
        public string LivroId { get; set; }
        public string UsuarioId { get; set; } = string.Empty;
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}