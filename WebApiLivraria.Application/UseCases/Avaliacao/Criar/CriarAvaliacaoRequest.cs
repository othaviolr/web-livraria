namespace WebApiLivraria.Application.UseCases.Avaliacao.Criar
{
    public class CriarAvaliacaoRequest
    {
        public int LivroId { get; set; }
        public int UsuarioId { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
    }
}
