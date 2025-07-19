using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.Usuarios.AtualizarPerfil
{
    public class AtualizarPerfilUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public AtualizarPerfilUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> ExecutarAsync(string usuarioId, AtualizarPerfilRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);
            if (usuario == null)
                return false;

            if (string.IsNullOrWhiteSpace(request.NomeUsuario))
                throw new ArgumentException("Nome de usuário é obrigatório.");

            if (string.IsNullOrWhiteSpace(request.Role))
                request.Role = "Leitor";

            try
            {
                usuario.AtualizarPerfil(request.NomeUsuario, request.FotoUrl, request.Cidade, request.Role, request.Bio);
                await _usuarioRepository.Atualizar(usuario);
                return true;
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException($"Falha ao atualizar perfil: {ex.Message}", ex);
            }
        }
    }
}