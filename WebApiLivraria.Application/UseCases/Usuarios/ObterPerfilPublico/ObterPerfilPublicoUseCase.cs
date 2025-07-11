using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Domain.Enums;

namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico
{
    public class ObterPerfilPublicoUseCase : IObterPerfilPublicoUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public ObterPerfilPublicoUseCase(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<UsuarioPerfilPublicoDto?> ExecutarAsync(string nomeUsuario)
        {
            var usuario = await _usuarioRepository.ObterPorNomeUsuarioComRelacionamentosAsync(nomeUsuario);
            if (usuario == null) return null;

            var livrosLidos = usuario.LivrosLidos
                .Where(l => l.Status == StatusLeitura.Lido)
                .OrderByDescending(l => l.DataAtualizacao)
                .Select(l => new LivroResumoDto
                {
                    Id = l.Livro.Id,
                    Titulo = l.Livro.Titulo,
                    Autor = l.Livro.Autor.Nome,
                    ImagemUrl = l.Livro.ImagemUrl
                })
                .ToList();

            var favoritos = usuario.Favoritos
                .OrderByDescending(f => f.DataCriacao)
                .Select(f => new LivroResumoDto
                {
                    Id = f.Livro.Id,
                    Titulo = f.Livro.Titulo,
                    Autor = f.Livro.Autor.Nome,
                    ImagemUrl = f.Livro.ImagemUrl
                })
                .ToList();

            var resenhas = usuario.Avaliacoes
          .OrderByDescending(a => a.DataCriacao)
          .Take(3)
          .Select(a => new ResenhaDto
          {
              LivroId = a.Livro?.Id ?? 0,
              Titulo = a.Livro?.Titulo ?? "Título desconhecido",
              Autor = a.Livro?.Autor?.Nome ?? "Autor desconhecido",
              ImagemUrl = string.IsNullOrEmpty(a.Livro?.ImagemUrl) ? "/default-book.png" : a.Livro.ImagemUrl,
              Nota = a.Nota,
              Comentario = a.Comentario,
              Data = a.DataCriacao
          })
          .ToList();

            return new UsuarioPerfilPublicoDto
            {
                Nome = usuario.Nome,
                NomeUsuario = usuario.NomeUsuario!,
                FotoUrl = usuario.FotoUrl,
                Bio = usuario.Bio,
                Cidade = usuario.Cidade,
                LivrosLidos = livrosLidos,
                Favoritos = favoritos,
                ResenhasRecentes = resenhas
            };
        }
    }
}