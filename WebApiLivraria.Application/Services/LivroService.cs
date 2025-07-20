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
        private readonly ISinopseRepository _sinopseRepository;

        public LivroService(
            ILivroRepository livroRepository,
            IGeneroRepository generoRepository,
            IAutorRepository autorRepository,
            IEditoraRepository editoraRepository,
            ISinopseRepository sinopseRepository)
        {
            _livroRepository = livroRepository;
            _generoRepository = generoRepository;
            _autorRepository = autorRepository;
            _editoraRepository = editoraRepository;
            _sinopseRepository = sinopseRepository;
        }

        public async Task<IEnumerable<LivroDto>> ListarAsync(string? search = null)
        {
            var livros = await _livroRepository.ListarComFiltroAsync(search);

            var autores = (await _autorRepository.ListarAsync())
                .ToDictionary(a => a.Id, a => a.Nome);

            var editoras = (await _editoraRepository.ListarAsync())
                .ToDictionary(e => e.Id, e => e.Nome);

            var livroIds = livros.Select(l => l.Id).ToList();
            var sinopses = (await _sinopseRepository.ListarPorLivroIdsAsync(livroIds))
                           .ToDictionary(s => s.LivroId, s => s.Texto);

            return livros.Select(l => new LivroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                AutorId = l.AutorId,
                AutorNome = autores.GetValueOrDefault(l.AutorId),
                EditoraId = l.EditoraId,
                EditoraNome = editoras.GetValueOrDefault(l.EditoraId),
                AnoPublicacao = l.AnoPublicacao,
                ImagemUrl = l.ImagemUrl,
                NumeroPaginas = l.NumeroPaginas,
                Idioma = l.Idioma,
                Generos = l.LivroGeneros.Select(g => g.GeneroId).ToList(),
                Sinopse = sinopses.GetValueOrDefault(l.Id)
            });
        }

        public async Task<LivroDto> ObterPorIdAsync(string id)
        {
            var livro = await _livroRepository.ObterPorIdAsync(id);
            if (livro == null) return null;

            var autor = await _autorRepository.ObterPorIdAsync(livro.AutorId);
            var editora = await _editoraRepository.ObterPorIdAsync(livro.EditoraId);
            var sinopse = await _sinopseRepository.ObterPorLivroIdAsync(id);

            return new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                AutorId = livro.AutorId,
                AutorNome = autor?.Nome,
                EditoraId = livro.EditoraId,
                EditoraNome = editora?.Nome,
                AnoPublicacao = livro.AnoPublicacao,
                ImagemUrl = livro.ImagemUrl,
                NumeroPaginas = livro.NumeroPaginas,
                Idioma = livro.Idioma,
                Generos = livro.LivroGeneros.Select(g => g.GeneroId).ToList(),
                Sinopse = sinopse?.Texto
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
                var sinopse = new Sinopse(livro.Id, dto.Sinopse);
                await _sinopseRepository.AdicionarAsync(sinopse);
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
                Sinopse = dto.Sinopse
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

            livroExistente.LimparGeneros();

            foreach (var generoId in dto.Generos ?? Enumerable.Empty<string>())
            {
                livroExistente.AdicionarGenero(generoId);
            }

            await _livroRepository.AtualizarAsync(livroExistente);

            var sinopseExistente = await _sinopseRepository.ObterPorLivroIdAsync(dto.Id);
            if (sinopseExistente == null && !string.IsNullOrWhiteSpace(dto.Sinopse))
            {
                var novaSinopse = new Sinopse(dto.Id, dto.Sinopse);
                await _sinopseRepository.AdicionarAsync(novaSinopse);
            }
            else if (sinopseExistente != null)
            {
                if (string.IsNullOrWhiteSpace(dto.Sinopse))
                {
                    await _sinopseRepository.RemoverPorLivroIdAsync(dto.Id);
                }
                else
                {
                    sinopseExistente.AtualizarTexto(dto.Sinopse);
                    await _sinopseRepository.AtualizarAsync(sinopseExistente);
                }
            }
        }

        public async Task RemoverAsync(string id)
        {
            await _livroRepository.RemoverAsync(id);
            await _sinopseRepository.RemoverPorLivroIdAsync(id);
        }
    }
}