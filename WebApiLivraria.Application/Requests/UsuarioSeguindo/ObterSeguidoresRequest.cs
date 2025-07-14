namespace WebApiLivraria.Application.Requests.UsuarioSeguindo
{
    public class ObterSeguidoresRequest
    {
        public Guid UsuarioId { get; set; }

        public ObterSeguidoresRequest(Guid usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}