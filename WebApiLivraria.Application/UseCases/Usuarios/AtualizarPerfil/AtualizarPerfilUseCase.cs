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

        public async Task<bool> ExecutarAsync(Guid usuarioId, AtualizarPerfilRequest request)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioId);

            if (usuario == null)
                return false;

            usuario.AtualizarPerfil(request.NomeUsuario, request.FotoUrl, request.Cidade, request.Role, request.Bio);

            await _usuarioRepository.Atualizar(usuario);
            return true;
        }
    }
}