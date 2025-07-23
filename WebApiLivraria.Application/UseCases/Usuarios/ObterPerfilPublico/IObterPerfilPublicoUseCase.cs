namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico
{
    public interface IObterPerfilPublicoUseCase
    {
        Task<UsuarioPerfilPublicoDto?> ExecutarAsync(string nomeUsuario, string? idUsuarioLogado = null);
    }
}