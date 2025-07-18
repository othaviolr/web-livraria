namespace WebApiLivraria.Application.UseCases.Usuarios.Login
{
    public class LoginUsuarioRequest
    {
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }

    public class LoginUsuarioResponse
    {
        public string Token { get; set; } = null!;
        public string UsuarioId { get; set; } = null!;
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
    }
}