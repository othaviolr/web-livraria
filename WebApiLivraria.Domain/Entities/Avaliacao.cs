using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiLivraria.Domain.Entities
{
    public class Avaliacao
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string LivroId { get; private set; }

        [BsonIgnore]
        public Livro Livro { get; private set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UsuarioId { get; private set; }

        [BsonIgnore]
        public Usuario Usuario { get; private set; } = null!;

        public int Nota { get; private set; }
        public string Comentario { get; private set; }
        public DateTime DataCriacao { get; private set; }

        public Avaliacao(string livroId, string usuarioId, int nota, string comentario)
        {
            if (string.IsNullOrWhiteSpace(livroId)) throw new ArgumentException("LivroId inválido.");
            if (string.IsNullOrWhiteSpace(usuarioId)) throw new ArgumentException("UsuarioId inválido.");
            if (nota < 1 || nota > 5) throw new ArgumentOutOfRangeException(nameof(nota), "Nota deve estar entre 1 e 5.");
            if (string.IsNullOrWhiteSpace(comentario)) throw new ArgumentException("Comentario é obrigatório.");

            Id = ObjectId.GenerateNewId().ToString();
            LivroId = livroId;
            UsuarioId = usuarioId;
            Nota = nota;
            Comentario = comentario;
            DataCriacao = DateTime.UtcNow;
        }

        public void Atualizar(int novaNota, string novoComentario)
        {
            if (novaNota < 1 || novaNota > 5) throw new ArgumentOutOfRangeException(nameof(novaNota), "Nota deve estar entre 1 e 5.");
            if (string.IsNullOrWhiteSpace(novoComentario)) throw new ArgumentException("Comentario é obrigatório.");

            Nota = novaNota;
            Comentario = novoComentario;
            DataCriacao = DateTime.UtcNow;
        }

        public void DefinirUsuario(Usuario usuario)
        {
            Usuario = usuario ?? throw new ArgumentNullException(nameof(usuario));
        }

        public void DefinirLivro(Livro livro)
        {
            Livro = livro ?? throw new ArgumentNullException(nameof(livro));
        }

        protected Avaliacao() { }
    }
}