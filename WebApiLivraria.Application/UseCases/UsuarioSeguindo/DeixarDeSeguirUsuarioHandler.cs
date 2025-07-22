using WebApiLivraria.Application.Requests.UsuarioSeguindo;
using WebApiLivraria.Domain.Repositories;
using MongoDB.Bson;

namespace WebApiLivraria.Application.UseCases.UsuarioSeguindo
{
    public class DeixarDeSeguirUsuarioHandler
    {
        private readonly IUsuarioSeguindoRepository _usuarioSeguindoRepository;

        public DeixarDeSeguirUsuarioHandler(IUsuarioSeguindoRepository usuarioSeguindoRepository)
        {
            _usuarioSeguindoRepository = usuarioSeguindoRepository;
        }

        public async Task HandleAsync(string usuarioAutenticadoId, DeixarDeSeguirRequest request)
        {
            if (!ObjectId.TryParse(usuarioAutenticadoId, out var usuarioAutenticadoObjectId))
                throw new ArgumentException("Id do usuário autenticado inválido.");

            if (!ObjectId.TryParse(request.UsuarioIdParaDeixarDeSeguir, out var usuarioParaDeixarObjectId))
                throw new ArgumentException("Id do usuário a deixar de seguir inválido.");

            if (usuarioAutenticadoObjectId == usuarioParaDeixarObjectId)
                throw new InvalidOperationException("Você não pode deixar de seguir a si mesmo.");

            var jaSegue = await _usuarioSeguindoRepository
                .ExisteRelacionamentoAsync(usuarioAutenticadoObjectId.ToString(), usuarioParaDeixarObjectId.ToString());

            if (!jaSegue)
                return;

            await _usuarioSeguindoRepository
                .DeixarDeSeguirAsync(usuarioAutenticadoObjectId.ToString(), usuarioParaDeixarObjectId.ToString());
        }
    }
}