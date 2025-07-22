using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace WebApiLivraria.Domain.Entities
{
    public class UsuarioSeguindo
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? Id { get; set; }

        [BsonRepresentation(BsonType.ObjectId)]
        public string SeguidorId { get; private set; } = null!;

        [BsonRepresentation(BsonType.ObjectId)]
        public string SeguindoId { get; private set; } = null!;

        public DateTime Data { get; private set; }

        [BsonIgnore]
        public Usuario? Seguidor { get; private set; }

        [BsonIgnore]
        public Usuario? Seguindo { get; private set; }

        private UsuarioSeguindo() { }

        public UsuarioSeguindo(string seguidorId, string seguindoId)
        {
            if (string.IsNullOrWhiteSpace(seguidorId)) throw new ArgumentException("SeguidorId inválido.");
            if (string.IsNullOrWhiteSpace(seguindoId)) throw new ArgumentException("SeguindoId inválido.");
            if (seguidorId == seguindoId)
                throw new ArgumentException("O usuário não pode seguir a si mesmo.");

            SeguidorId = seguidorId;
            SeguindoId = seguindoId;
            Data = DateTime.UtcNow;
        }

        public void DefinirSeguidor(Usuario seguidor)
        {
            Seguidor = seguidor;
        }

        public void DefinirSeguindo(Usuario seguindo)
        {
            Seguindo = seguindo;
        }
    }
}