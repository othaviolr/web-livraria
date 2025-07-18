using WebApiLivraria.Domain.Enums;

namespace WebApiLivraria.Application.Dto
{
    public class AtualizarLeituraDto
    {
        public string LivroId { get; set; } = null!;
        public StatusLeitura Status { get; set; }
    }
}