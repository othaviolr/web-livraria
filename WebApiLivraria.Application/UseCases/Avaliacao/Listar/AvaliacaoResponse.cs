using System;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Listar
{
    public class AvaliacaoResponse
    {
        public int Id { get; set; }
        public int LivroId { get; set; }
        public Guid UsuarioId { get; set; }
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime DataAvaliacao { get; set; }
    }
}