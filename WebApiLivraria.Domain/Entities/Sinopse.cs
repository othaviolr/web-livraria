using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiLivraria.Domain.Entities
{
    public class Sinopse
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = ObjectId.GenerateNewId().ToString();

        [BsonRepresentation(BsonType.ObjectId)]
        public string LivroId { get; private set; } 

        [BsonElement("texto")]
        public string Texto { get; private set; } = null!;

        protected Sinopse() { }

        public Sinopse(string livroId, string texto)
        {
            if (string.IsNullOrWhiteSpace(livroId))
                throw new ArgumentException("LivroId é obrigatório.");
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("Texto da sinopse é obrigatório.");

            LivroId = livroId;
            Texto = texto;
        }

        public void AtualizarTexto(string texto)
        {
            if (string.IsNullOrWhiteSpace(texto))
                throw new ArgumentException("Texto da sinopse é obrigatório.");

            Texto = texto;
        }
    }
}