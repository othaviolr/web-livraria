using WebApiLivraria.Application.Requests.UsuarioSeguindo;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.UsuarioSeguindo
{
    public class DeixarDeSeguirUsuarioHandler
    {
        private readonly IUsuarioSeguindoRepository _usuarioSeguindoRepository;

        public DeixarDeSeguirUsuarioHandler(IUsuarioSeguindoRepository usuarioSeguindoRepository)
        {
            _usuarioSeguindoRepository = usuarioSeguindoRepository;
        }

        public async Task HandleAsync(Guid usuarioAutenticadoId, DeixarDeSeguirRequest request)
        {
            if (usuarioAutenticadoId == request.UsuarioIdParaDeixarDeSeguir)
                throw new InvalidOperationException("Você não pode deixar de seguir a si mesmo.");

            var jaSegue = await _usuarioSeguindoRepository
                .ExisteRelacionamentoAsync(usuarioAutenticadoId, request.UsuarioIdParaDeixarDeSeguir);

            if (!jaSegue)
                return;

            await _usuarioSeguindoRepository
                .DeixarDeSeguirAsync(usuarioAutenticadoId, request.UsuarioIdParaDeixarDeSeguir);
        }
    }
}