using WebApiLivraria.Application.Requests.UsuarioSeguindo;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.UsuarioSeguindo
{
    public class SeguirUsuarioHandler
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioSeguindoRepository _usuarioSeguindoRepository;

        public SeguirUsuarioHandler(
            IUsuarioRepository usuarioRepository,
            IUsuarioSeguindoRepository usuarioSeguindoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioSeguindoRepository = usuarioSeguindoRepository;
        }

        public async Task HandleAsync(Guid usuarioAutenticadoId, SeguirUsuarioRequest request)
        {
            if (usuarioAutenticadoId == request.UsuarioIdParaSeguir)
                throw new InvalidOperationException("Você não pode seguir a si mesmo.");

            var existeUsuario = await _usuarioRepository.ExistePorIdAsync(request.UsuarioIdParaSeguir);
            if (!existeUsuario)
                throw new InvalidOperationException("Usuário que você está tentando seguir não existe.");

            var jaSegue = await _usuarioSeguindoRepository
                .ExisteRelacionamentoAsync(usuarioAutenticadoId, request.UsuarioIdParaSeguir);

            if (jaSegue)
                throw new InvalidOperationException("Você já está seguindo este usuário.");

            var usuarioSeguindo = new Domain.Entities.UsuarioSeguindo(
                usuarioAutenticadoId,
                request.UsuarioIdParaSeguir
                );

            await _usuarioSeguindoRepository.SeguirAsync(usuarioSeguindo);
        }
    }
}