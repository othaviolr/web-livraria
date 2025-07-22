using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiLivraria.Domain.Entities
{
    public class ListaDesejo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UsuarioId { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string LivroId { get; private set; }

        public DateTime DataCriacao { get; private set; }

        [BsonIgnore]
        public Usuario Usuario { get; private set; } = null!;

        [BsonIgnore]
        public Livro Livro { get; private set; } = null!;

        protected ListaDesejo() { }

        public ListaDesejo(string usuarioId, string livroId)
        {
            if (string.IsNullOrWhiteSpace(usuarioId)) throw new ArgumentException("UsuarioId inválido.");
            if (string.IsNullOrWhiteSpace(livroId)) throw new ArgumentException("LivroId inválido.");

            Id = ObjectId.GenerateNewId().ToString();
            UsuarioId = usuarioId;
            LivroId = livroId;
            DataCriacao = DateTime.UtcNow;
        }

        public void DefinirLivro(Livro livro)
        {
            this.Livro = livro;
        }
    }
}