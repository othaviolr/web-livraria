using WebApiLivraria.Domain.Enums;

namespace WebApiLivraria.Application.Dto
{
    public class AtualizarLeituraDto
    {
        public int LivroId { get; set; }
        public StatusLeitura Status { get; set; }
    }
}