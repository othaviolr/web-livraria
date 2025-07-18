using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiLivraria.Domain.Entities
{
    public class RankingLivro
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; } = null!;

        public string Genero { get; set; } = null!;

        public int Posicao { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string LivroId { get; set; } = null!;

        [BsonIgnore]
        public Livro Livro { get; set; } = null!;

        public double NotaMedia { get; set; }

        public int TotalAvaliacoes { get; set; }

        public DateTime DataAtualizacao { get; set; }
    }
}