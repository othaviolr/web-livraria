using System;

namespace WebApiLivraria.Domain.Entities
{
    public class Avaliacao
    {
        public int Id { get; private set; }
        public int LivroId { get; private set; }
        public Livro Livro { get; private set; }
        public int UsuarioId { get; private set; }
        public int Nota { get; private set; }
        public string Comentario { get; private set; }
        public DateTime DataAvaliacao { get; private set; }

        public Avaliacao(int livroId, int usuarioId, int nota, string comentario)
        {
            LivroId = livroId;
            UsuarioId = usuarioId;
            Nota = nota;
            Comentario = comentario;
            DataAvaliacao = DateTime.UtcNow;
        }

        protected Avaliacao() { }
    }
}
