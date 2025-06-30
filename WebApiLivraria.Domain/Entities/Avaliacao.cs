using System;

namespace WebApiLivraria.Domain.Entities
{
    public class Avaliacao
    {
        public int Id { get; private set; }
        public int LivroId { get; private set; }
        public Livro Livro { get; private set; }

        public Guid UsuarioId { get; private set; }
        public Usuario Usuario { get; private set; }

        public int Nota { get; private set; }
        public string Comentario { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public Avaliacao(int livroId, Guid usuarioId, int nota, string comentario)
        {
            LivroId = livroId;
            UsuarioId = usuarioId;
            Nota = nota;
            Comentario = comentario;
            DataCriacao = DateTime.UtcNow;
        }

        public void Atualizar(int novaNota, string novoComentario)
        {
            Nota = novaNota;
            Comentario = novoComentario;
            DataCriacao = DateTime.UtcNow;
        }

        protected Avaliacao() { }
    }
}