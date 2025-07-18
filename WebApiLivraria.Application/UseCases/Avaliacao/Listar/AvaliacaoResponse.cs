using System;
using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Listar
{
    public class AvaliacaoResponse
    {
        public string Id { get; set; } = string.Empty;       
        public string LivroId { get; set; } = string.Empty;   
        public int Nota { get; set; }
        public string Comentario { get; set; } = string.Empty;
        public DateTime DataAvaliacao { get; set; }

        public UsuarioResumoDto Usuario { get; set; } = null!;
    }
}