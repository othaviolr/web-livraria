using WebApiLivraria.Application.Dto;
using WebApiLivraria.Domain.Enums;
using WebApiLivraria.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLivraria.Application.UseCases.Usuarios.ObterPerfilPublico
{
    public class ObterPerfilPublicoUseCase : IObterPerfilPublicoUseCase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IUsuarioSeguindoRepository _usuarioSeguindoRepository;

        public ObterPerfilPublicoUseCase(
            IUsuarioRepository usuarioRepository,
            IUsuarioSeguindoRepository usuarioSeguindoRepository)
        {
            _usuarioRepository = usuarioRepository;
            _usuarioSeguindoRepository = usuarioSeguindoRepository;
        }

        public async Task<UsuarioPerfilPublicoDto?> ExecutarAsync(string nomeUsuario, string? usuarioLogadoId = null)
        {
            var usuario = await _usuarioRepository.ObterPorNomeUsuarioComRelacionamentosAsync(nomeUsuario);
            if (usuario == null) return null;

            var livrosLidos = usuario.LivrosLidos?
                .Where(l => l.Status == StatusLeitura.Lido && l.Livro != null && l.Livro.Autor != null)
                .OrderByDescending(l => l.DataAtualizacao)
                .Select(l => new LivroResumoDto
                {
                    Id = l.Livro!.Id,
                    Titulo = l.Livro.Titulo,
                    Autor = l.Livro.Autor!.Nome,
                    ImagemUrl = l.Livro.ImagemUrl,
                    StatusLeitura = l.Status
                })
                .ToList() ?? new List<LivroResumoDto>();

            var favoritos = usuario.Favoritos?
                .Where(f => f.Livro != null && f.Livro.Autor != null)
                .OrderByDescending(f => f.DataCriacao)
                .Select(f => new LivroResumoDto
                {
                    Id = f.Livro!.Id,
                    Titulo = f.Livro.Titulo,
                    Autor = f.Livro.Autor?.Nome ?? "Autor desconhecido",
                    ImagemUrl = f.Livro.ImagemUrl,
                    StatusLeitura = StatusLeitura.Lido
                })
                .ToList() ?? new List<LivroResumoDto>();

            var wishlist = usuario.ListasDesejo?
                .Where(w => w.Livro != null && w.Livro.Autor != null)
                .OrderByDescending(w => w.DataCriacao)
                .Select(w => new LivroResumoDto
                {
                    Id = w.Livro!.Id,
                    Titulo = w.Livro.Titulo,
                    Autor = w.Livro.Autor!.Nome,
                    ImagemUrl = w.Livro.ImagemUrl,
                    StatusLeitura = StatusLeitura.QueroLer
                })
                .ToList() ?? new List<LivroResumoDto>();

            var resenhas = usuario.Avaliacoes?
                .OrderByDescending(a => a.DataCriacao)
                .Take(3)
                .Select(a => new ResenhaDto
                {
                    LivroId = a.Livro?.Id ?? string.Empty,
                    Titulo = a.Livro?.Titulo ?? "Título desconhecido",
                    Autor = a.Livro?.Autor?.Nome ?? "Autor desconhecido",
                    ImagemUrl = string.IsNullOrEmpty(a.Livro?.ImagemUrl) ? "/default-book.png" : a.Livro!.ImagemUrl,
                    Nota = a.Nota,
                    Comentario = a.Comentario,
                    Data = a.DataCriacao
                })
                .ToList() ?? new List<ResenhaDto>();

            var statusLeituraContagem = usuario.LivrosLidos?
                .GroupBy(l => l.Status)
                .ToDictionary(g => g.Key, g => g.Count())
                ?? new Dictionary<StatusLeitura, int>();

            var totalResenhas = usuario.Avaliacoes?.Count() ?? 0;

            var seguidores = usuario.Seguidores?
                .Select(s => s.Seguidor)
                .Where(u => u != null)
                .Select(u => new UsuarioResumoDto
                {
                    Nome = u!.Nome,
                    NomeUsuario = u.NomeUsuario,
                    FotoUrl = u.FotoUrl,
                    Bio = u.Bio
                })
                .ToList() ?? new List<UsuarioResumoDto>();

            var seguindo = usuario.Seguindo?
                .Select(s => s.Seguindo)
                .Where(u => u != null)
                .Select(u => new UsuarioResumoDto
                {
                    Nome = u!.Nome,
                    NomeUsuario = u.NomeUsuario,
                    FotoUrl = u.FotoUrl,
                    Bio = u.Bio
                })
                .ToList() ?? new List<UsuarioResumoDto>();

            bool seguindoAtualmente = false;
            if (!string.IsNullOrEmpty(usuarioLogadoId) && usuarioLogadoId != usuario.Id)
            {
                seguindoAtualmente = await _usuarioSeguindoRepository.VerificarSeSegueAsync(usuarioLogadoId, usuario.Id);
            }

            return new UsuarioPerfilPublicoDto
            {
                Id = usuario.Id.ToString(),
                Nome = usuario.Nome,
                NomeUsuario = usuario.NomeUsuario ?? string.Empty,
                FotoUrl = usuario.FotoUrl,
                Bio = usuario.Bio,
                Cidade = usuario.Cidade,

                LivrosLidos = livrosLidos,
                Favoritos = favoritos,
                Wishlist = wishlist,
                ResenhasRecentes = resenhas,
                Seguidores = seguidores,
                Seguindo = seguindo,

                TotalLido = statusLeituraContagem.GetValueOrDefault(StatusLeitura.Lido),
                TotalLendo = statusLeituraContagem.GetValueOrDefault(StatusLeitura.Lendo),
                TotalQueroLer = statusLeituraContagem.GetValueOrDefault(StatusLeitura.QueroLer),
                TotalRelendo = statusLeituraContagem.GetValueOrDefault(StatusLeitura.Relendo),
                TotalAbandonei = statusLeituraContagem.GetValueOrDefault(StatusLeitura.Abandonei),
                TotalResenhas = totalResenhas,

                SeguindoAtualmente = seguindoAtualmente
            };
        }
    }
}