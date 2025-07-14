namespace WebApiLivraria.Application.Requests.UsuarioSeguindo
{
    public class ObterSeguindoRequest
    {
        public Guid UsuarioId { get; set; }

        public ObterSeguindoRequest(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}