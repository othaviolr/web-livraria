namespace WebApiLivraria.Domain.Entities
{
    public class UsuarioSeguindo
    {
        public Guid SeguidorId { get; private set; } 
        public Guid SeguindoId { get; private set; }
        public DateTime Data { get; private set; }

        public Usuario? Seguidor { get; private set; }
        public Usuario? Seguindo { get; private set; }

        private UsuarioSeguindo() { }

        public UsuarioSeguindo(Guid seguidorId, Guid seguindoId)
        {
            if (seguidorId == seguindoId)
                throw new ArgumentException("O usuário não pode seguir a si mesmo.");

            SeguidorId = seguidorId;
            SeguindoId = seguindoId;
            Data = DateTime.UtcNow;
        }
    }
}