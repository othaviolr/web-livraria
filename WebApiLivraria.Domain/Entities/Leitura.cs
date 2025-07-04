using WebApiLivraria.Domain.Enums;

namespace WebApiLivraria.Domain.Entities
{
    public class Leitura
    {
        public Guid Id { get; private set; }
        public Guid UsuarioId { get; private set; }
        public int LivroId { get; private set; }
        public StatusLeitura Status { get; private set; }
        public DateTime DataAtualizacao { get; private set; }

        public virtual Livro Livro { get; private set; }
        public virtual Usuario Usuario { get; private set; }

        protected Leitura() { }

        public Leitura(Guid usuarioId, int livroId, StatusLeitura status)
        {
            Id = Guid.NewGuid();
            UsuarioId = usuarioId;
            LivroId = livroId;
            Status = status;
            DataAtualizacao = DateTime.UtcNow;
        }

        public void AtualizarStatus(StatusLeitura novoStatus)
        {
            Status = novoStatus;
            DataAtualizacao = DateTime.UtcNow;
        }
    }
}