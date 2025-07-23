using WebApiLivraria.Domain.Enums;

namespace WebApiLivraria.Application.Dto
{
    public class LivroMarcadoDto
    {
        public string LivroId { get; set; } = null!;
        public string Titulo { get; set; } = null!;
        public string ImagemUrl { get; set; } = null!;
        public string Autor { get; set; } = null!;
        public StatusLeitura Status { get; set; }
    }
}