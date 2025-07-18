using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiLivraria.Domain.Entities
{
    public class LivroGenero
    {
        [BsonRepresentation(BsonType.ObjectId)]
        public string LivroId { get; private set; } = null!;

        [BsonIgnore]
        public Livro Livro { get; private set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string GeneroId { get; private set; } = null!;

        [BsonIgnore]
        public Genero Genero { get; private set; } = null!;

        protected LivroGenero() { }

        public LivroGenero(string livroId, string generoId)
        {
            if (string.IsNullOrWhiteSpace(livroId)) throw new ArgumentException("LivroId inválido.");
            if (string.IsNullOrWhiteSpace(generoId)) throw new ArgumentException("GeneroId inválido.");

            LivroId = livroId;
            GeneroId = generoId;
        }
    }
}