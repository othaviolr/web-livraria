using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.Usuarios.Excluir
{
    public class ExcluirUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ExcluirUsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> ExecutarAsync(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorId(usuarioId.ToString());
            if (usuario == null)
                return false;

            await _usuarioRepository.Remover(usuario);
            return true;
        }
    }
}