namespace WebApiLivraria.Application.Requests.UsuarioSeguindo
{
    public class ObterSeguidoresRequest
    {
        public string UsuarioId { get; set; }

        public ObterSeguidoresRequest(string usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}