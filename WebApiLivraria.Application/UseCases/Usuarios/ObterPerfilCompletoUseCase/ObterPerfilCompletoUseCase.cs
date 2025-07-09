using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico;
using WebApiLivraria.Domain.Enums;
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
            var usuario = await _usuarioRepository.ObterPorIdComDetalhesAsync(usuarioId);

            if (usuario == null)
                throw new Exception("Usuário não encontrado");

            var contagemStatus = await _usuarioRepository.ObterContagemLivrosPorStatusAsync(usuarioId);

            var livrosMarcados = usuario.LivrosLidos.Select(l => new LivroResumoDto
            {
                Id = l.Livro.Id,
                Titulo = l.Livro.Titulo,
                Autor = l.Livro.Autor.Nome,
                ImagemUrl = l.Livro.ImagemUrl,
                StatusLeitura = l.Status
            }).ToList();

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
                Bio = usuario.Bio,

                QuantidadeFavoritos = contagemStatus.TryGetValue(StatusLeitura.QueroLer, out var favoritos) ? favoritos : 0,
                QuantidadeQueroLer = contagemStatus.TryGetValue(StatusLeitura.QueroLer, out var queroLer) ? queroLer : 0,
                QuantidadeLendo = contagemStatus.TryGetValue(StatusLeitura.Lendo, out var lendo) ? lendo : 0,
                QuantidadeLido = contagemStatus.TryGetValue(StatusLeitura.Lido, out var lido) ? lido : 0,
                QuantidadeAbandonei = contagemStatus.TryGetValue(StatusLeitura.Abandonei, out var abandonei) ? abandonei : 0,
                QuantidadeRelendo = contagemStatus.TryGetValue(StatusLeitura.Relendo, out var relendo) ? relendo : 0,

                LivrosMarcados = livrosMarcados,
                Atividades = atividades
            };
        }
    }
}