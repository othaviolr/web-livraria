using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Application.UseCases.Usuarios.RegistrarUsuario
{
    public class RegistrarUsuarioUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public RegistrarUsuarioUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<string> Executar(RegistrarUsuarioRequest request)
        {
            var usuarioExistente = await _usuarioRepository.ObterPorEmail(request.Email);
            if (usuarioExistente != null)
                throw new Exception("Já existe um usuário com este e-mail.");

            var senhaHash = BCrypt.Net.BCrypt.HashPassword(request.Senha);
            var novoUsuario = new Usuario(request.Nome, request.Email, senhaHash);

            await _usuarioRepository.Adicionar(novoUsuario);

            return novoUsuario.Id;
        }
    }
}