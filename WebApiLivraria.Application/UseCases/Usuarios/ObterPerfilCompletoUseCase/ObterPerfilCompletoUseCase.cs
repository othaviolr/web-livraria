using WebApiLivraria.Application.Dto;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilCompletoUseCase
{
    public class ObterPerfilCompletoUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ObterPerfilCompletoUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioPerfilDto> ExecutarAsync(Guid usuarioId)
        {
            var usuario = await _usuarioRepository.ObterPorIdComAvaliacoesAsync(usuarioId);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            var atividades = usuario.Avaliacoes
                .OrderByDescending(a => a.DataCriacao)
                .Take(10)
                .Select(a => new AtividadeDto
                {
                    Tipo = "Avaliação",
                    Descricao = $"Avaliou o livro '{a.Livro.Titulo}' com nota {a.Nota}",
                    Data = a.DataCriacao
                })
                .ToList();

            return new UsuarioPerfilDto
            {
                NomeUsuario = usuario.NomeUsuario ?? usuario.Nome,
                FotoUrl = usuario.FotoUrl,
                Cidade = usuario.Cidade,
                Role = usuario.Role,
                Atividades = atividades
            };
        }
    }
}