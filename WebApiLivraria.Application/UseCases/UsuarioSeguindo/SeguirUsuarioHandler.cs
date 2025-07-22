using WebApiLivraria.Application.Requests.UsuarioSeguindo;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico;
using WebApiLivraria.Domain.Repositories;
using MongoDB.Bson;

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
            if (!ObjectId.TryParse(usuarioAutenticadoId, out var usuarioAutenticadoObjectId))
                throw new ArgumentException("Id do usuário autenticado inválido.");

            if (!ObjectId.TryParse(request.UsuarioIdParaSeguir, out var usuarioParaSeguirObjectId))
                throw new ArgumentException("Id do usuário a seguir inválido.");

            if (usuarioAutenticadoObjectId == usuarioParaSeguirObjectId)
                throw new InvalidOperationException("Você não pode seguir a si mesmo.");

            var existeUsuario = await _usuarioRepository.ExistePorIdAsync(usuarioParaSeguirObjectId.ToString());
            if (!existeUsuario)
                throw new InvalidOperationException("Usuário que você está tentando seguir não existe.");

            var jaSegue = await _usuarioSeguindoRepository
                .ExisteRelacionamentoAsync(usuarioAutenticadoObjectId.ToString(), usuarioParaSeguirObjectId.ToString());

            if (jaSegue)
                throw new InvalidOperationException("Você já está seguindo este usuário.");

            var usuarioSeguindo = new Domain.Entities.UsuarioSeguindo(
                usuarioAutenticadoObjectId.ToString(),
                usuarioParaSeguirObjectId.ToString()
            );

            await _usuarioSeguindoRepository.SeguirAsync(usuarioSeguindo);
        }
    }
}