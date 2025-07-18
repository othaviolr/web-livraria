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

        public async Task HandleAsync(string usuarioAutenticadoId, SeguirUsuarioRequest request)
        {
            if (!Guid.TryParse(usuarioAutenticadoId, out var usuarioAutIdGuid))
                throw new ArgumentException("Id do usuário autenticado inválido.");

            if (!Guid.TryParse(request.UsuarioIdParaSeguir, out var usuarioParaSeguirGuid))
                throw new ArgumentException("Id do usuário a seguir inválido.");

            if (usuarioAutIdGuid == usuarioParaSeguirGuid)
                throw new InvalidOperationException("Você não pode seguir a si mesmo.");

            var existeUsuario = await _usuarioRepository.ExistePorIdAsync(usuarioParaSeguirGuid);
            if (!existeUsuario)
                throw new InvalidOperationException("Usuário que você está tentando seguir não existe.");

            var jaSegue = await _usuarioSeguindoRepository
                .ExisteRelacionamentoAsync(usuarioAutIdGuid, usuarioParaSeguirGuid);

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