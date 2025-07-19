using WebApiLivraria.Application.Dto;
using WebApiLivraria.Domain.Entities;
using System.Linq;

namespace WebApiLivraria.Application.Mappers
{
    public static class LivroMapper
    {
        public static LivroDto MapearParaDto(Livro livro)
        {
            return new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                AutorId = livro.AutorId,
                AutorNome = livro.Autor?.Nome,
                EditoraId = livro.EditoraId,
                EditoraNome = livro.Editora?.Nome,
                AnoPublicacao = livro.AnoPublicacao,
                ImagemUrl = livro.ImagemUrl,
                NumeroPaginas = livro.NumeroPaginas,
                Idioma = livro.Idioma,
                Sinopse = livro.Sinopse?.Texto,
                Generos = livro.LivroGeneros.Select(lg => lg.GeneroId).ToList()
            };
        }

        public static Livro MapearParaEntidade(LivroDto dto)
        {
            var livro = new Livro(
                dto.Titulo,
                dto.AutorId,
                dto.EditoraId,
                dto.AnoPublicacao,
                dto.NumeroPaginas,
                dto.Idioma ?? string.Empty,
                dto.ImagemUrl
            );

            if (!string.IsNullOrWhiteSpace(dto.Sinopse))
            {
                livro.AtualizarSinopse(dto.Sinopse);
            }

            if (dto.Generos != null)
            {
                foreach (var generoId in dto.Generos)
                {
                    livro.AdicionarGenero(generoId);
                }
            }

            return livro;
        }
    }
}