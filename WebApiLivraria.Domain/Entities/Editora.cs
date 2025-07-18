using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Collections.Generic;

namespace WebApiLivraria.Domain.Entities
{
    public class Editora
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = null!;

        public string Nome { get; private set; }
        public string Biografia { get; private set; }
        public string ImagemUrl { get; private set; }

        [BsonIgnore]
        public ICollection<Autor> Autores { get; private set; } = new List<Autor>();

        protected Editora() { }

        public Editora(string nome, string biografia, string imagemUrl)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");
            if (string.IsNullOrWhiteSpace(biografia)) throw new ArgumentException("Biografia é obrigatória.");
            if (string.IsNullOrWhiteSpace(imagemUrl)) throw new ArgumentException("Imagem é obrigatória.");

            Nome = nome;
            Biografia = biografia;
            ImagemUrl = imagemUrl;
        }

        public void AtualizarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");
            Nome = nome;
        }

        public void AtualizarBiografia(string biografia)
        {
            if (string.IsNullOrWhiteSpace(biografia)) throw new ArgumentException("Biografia é obrigatória.");
            Biografia = biografia;
        }

        public void AtualizarImagemUrl(string imagemUrl)
        {
            if (string.IsNullOrWhiteSpace(imagemUrl)) throw new ArgumentException("Imagem é obrigatória.");
            ImagemUrl = imagemUrl;
        }

        public void SetId(string id)
        {
            Id = id;
        }
    }
}