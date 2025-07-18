using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;

namespace WebApiLivraria.Application.Services
{
    public class LivroService : ILivroService
    {
        private readonly ILivroRepository _livroRepository;
        private readonly IGeneroRepository _generoRepository;
        private readonly IAutorRepository _autorRepository;
        private readonly IEditoraRepository _editoraRepository;

        public LivroService(
            ILivroRepository livroRepository,
            IGeneroRepository generoRepository,
            IAutorRepository autorRepository,
            IEditoraRepository editoraRepository)
        {
            _livroRepository = livroRepository;
            _generoRepository = generoRepository;
            _autorRepository = autorRepository;
            _editoraRepository = editoraRepository;
        }

        public async Task<IEnumerable<LivroDto>> ListarAsync(string? search = null)
        {
            var livros = await _livroRepository.ListarComFiltroAsync(search);

            return livros.Select(l => new LivroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                AutorId = l.AutorId,
                AutorNome = l.Autor?.Nome,
                EditoraId = l.EditoraId,
                EditoraNome = l.Editora?.Nome,
                AnoPublicacao = l.AnoPublicacao,
                ImagemUrl = l.ImagemUrl,
                NumeroPaginas = l.NumeroPaginas,
                Idioma = l.Idioma,
                Generos = l.LivroGeneros.Select(g => g.GeneroId).ToList(),
                Sinopse = l.Sinopse?.Texto
            });
        }

        public async Task<LivroDto> ObterPorIdAsync(string id)
        {
            var livro = await _livroRepository.ObterPorIdAsync(id);
            if (livro == null) return null;

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
                Generos = livro.LivroGeneros.Select(g => g.GeneroId).ToList(),
                Sinopse = livro.Sinopse?.Texto
            };
        }

        public async Task<LivroDto> AdicionarAsync(LivroDto dto)
        {
            var autor = await _autorRepository.ObterPorIdAsync(dto.AutorId);
            if (autor == null)
                throw new Exception($"Autor com Id {dto.AutorId} não encontrado.");

            var editora = await _editoraRepository.ObterPorIdAsync(dto.EditoraId);
            if (editora == null)
                throw new Exception($"Editora com Id {dto.EditoraId} não encontrada.");

            var generosValidos = await _generoRepository.ListarPorIdsAsync(dto.Generos ?? Enumerable.Empty<string>());
            if (generosValidos == null || !generosValidos.Any())
                throw new Exception("Gêneros inválidos ou não encontrados.");

            var livro = new Livro(
                dto.Titulo,
                dto.AutorId,
                dto.EditoraId,
                dto.AnoPublicacao,
                dto.NumeroPaginas,
                dto.Idioma,
                dto.ImagemUrl);

            foreach (var generoId in dto.Generos ?? Enumerable.Empty<string>())
            {
                if (generosValidos.Any(g => g.Id == generoId))
                    livro.AdicionarGenero(generoId);
            }

            await _livroRepository.AdicionarAsync(livro);

            if (!string.IsNullOrWhiteSpace(dto.Sinopse))
            {
                livro.AtualizarSinopse(dto.Sinopse);
                await _livroRepository.AtualizarAsync(livro);
            }

            return new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                AutorId = autor.Id,
                AutorNome = autor.Nome,
                EditoraId = editora.Id,
                EditoraNome = editora.Nome,
                AnoPublicacao = livro.AnoPublicacao,
                ImagemUrl = livro.ImagemUrl,
                NumeroPaginas = livro.NumeroPaginas,
                Idioma = livro.Idioma,
                Generos = livro.LivroGeneros.Select(g => g.GeneroId).ToList(),
                Sinopse = livro.Sinopse?.Texto
            };
        }

        public async Task AtualizarAsync(LivroDto dto)
        {
            var livroExistente = await _livroRepository.ObterPorIdAsync(dto.Id);
            if (livroExistente == null) return;

            livroExistente.AtualizarTitulo(dto.Titulo);
            livroExistente.AtualizarAutor(dto.AutorId);
            livroExistente.AtualizarEditora(dto.EditoraId);
            livroExistente.AtualizarAnoPublicacao(dto.AnoPublicacao);
            livroExistente.AtualizarImagemUrl(dto.ImagemUrl);
            livroExistente.AtualizarNumeroPaginas(dto.NumeroPaginas);
            livroExistente.AtualizarIdioma(dto.Idioma);

            if (!string.IsNullOrWhiteSpace(dto.Sinopse))
            {
                livroExistente.AtualizarSinopse(dto.Sinopse);
            }

            livroExistente.LimparGeneros();

            foreach (var generoId in dto.Generos ?? Enumerable.Empty<string>())
            {
                livroExistente.AdicionarGenero(generoId);
            }

            await _livroRepository.AtualizarAsync(livroExistente);
        }

        public async Task RemoverAsync(string id)
        {
            await _livroRepository.RemoverAsync(id);
        }
    }
}