namespace WebApiLivraria.Application.Requests.UsuarioSeguindo
{
    public class ObterSeguindoRequest
    {
        public string UsuarioId { get; set; }

        public ObterSeguindoRequest(string usuarioId)
        {
            UsuarioId = usuarioId;
        }
    }
}