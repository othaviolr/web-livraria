using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiLivraria.Domain.Entities
{
    public class Autor
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; private set; } = null!;

        public string Nome { get; private set; }

        public string? Biografia { get; private set; }

        public DateTime? DataNascimento { get; private set; }

        public string? LocalNascimento { get; private set; }

        public string? FotoUrl { get; private set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string? EditoraId { get; private set; }

        [BsonIgnore]
        public ICollection<Livro> Livros { get; private set; } = new List<Livro>();

        protected Autor() { }

        public Autor(
            string nome,
            string? editoraId = null,
            string? biografia = null,
            DateTime? dataNascimento = null,
            string? localNascimento = null,
            string? fotoUrl = null)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");

            Id = ObjectId.GenerateNewId().ToString();
            Nome = nome;
            EditoraId = editoraId;
            Biografia = biografia;
            DataNascimento = dataNascimento;
            LocalNascimento = localNascimento;
            FotoUrl = fotoUrl;
        }

        public void SetId(string id)
        {
            Id = id;
        }

        public void Atualizar(
            string nome,
            string? editoraId,
            string? biografia,
            DateTime? dataNascimento,
            string? localNascimento,
            string? fotoUrl)
        {
            if (string.IsNullOrWhiteSpace(nome)) throw new ArgumentException("Nome é obrigatório.");

            Nome = nome;
            EditoraId = editoraId;
            Biografia = biografia;
            DataNascimento = dataNascimento;
            LocalNascimento = localNascimento;
            FotoUrl = fotoUrl;
        }
    }
}