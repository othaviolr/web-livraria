using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using WebApiLivraria.Domain.Enums;

namespace WebApiLivraria.Domain.Entities
{
    public class Leitura
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string UsuarioId { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string LivroId { get; private set; }

        public StatusLeitura Status { get; private set; }

        public DateTime DataAtualizacao { get; private set; }

        [BsonIgnore]
        public virtual Livro Livro { get; private set; } = null!;

        [BsonIgnore]
        public virtual Usuario Usuario { get; private set; } = null!;

        protected Leitura() { }

        public Leitura(string usuarioId, string livroId, StatusLeitura status)
        {
            if (string.IsNullOrWhiteSpace(usuarioId)) throw new ArgumentException("UsuarioId inválido.");
            if (string.IsNullOrWhiteSpace(livroId)) throw new ArgumentException("LivroId inválido.");

            Id = ObjectId.GenerateNewId().ToString();
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

        public void DefinirLivro(Livro livro)
        {
            Livro = livro ?? throw new ArgumentNullException(nameof(livro));
        }
    }
}