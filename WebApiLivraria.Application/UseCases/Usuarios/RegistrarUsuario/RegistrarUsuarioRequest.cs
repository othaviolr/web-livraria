namespace WebApiLivraria.Application.UseCases.Usuarios.RegistrarUsuario
{
    public class RegistrarUsuarioRequest
    {
        public string Nome { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Senha { get; set; } = null!;
    }
}