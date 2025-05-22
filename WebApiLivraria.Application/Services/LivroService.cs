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

        public LivroService(ILivroRepository livroRepository, IGeneroRepository generoRepository)
        {
            _livroRepository = livroRepository;
            _generoRepository = generoRepository;
        }

        public async Task<IEnumerable<LivroDto>> ListarAsync()
        {
            var livros = await _livroRepository.ListarAsync();
            return livros.Select(l => new LivroDto
            {
                Id = l.Id,
                Titulo = l.Titulo,
                AutorId = l.AutorId,
                EditoraId = l.EditoraId,
                Generos = l.LivroGeneros.Select(g => g.GeneroId).ToList()
            });
        }

        public async Task<LivroDto> ObterPorIdAsync(int id)
        {
            var livro = await _livroRepository.ObterPorIdAsync(id);
            if (livro == null) return null;

            return new LivroDto
            {
                Id = livro.Id,
                Titulo = livro.Titulo,
                AutorId = livro.AutorId,
                EditoraId = livro.EditoraId,
                Generos = livro.LivroGeneros.Select(g => g.GeneroId).ToList()
            };
        }

        public async Task AdicionarAsync(LivroDto dto)
        {
            var livro = new Livro(dto.Titulo, dto.AutorId, dto.EditoraId);

            // Adiciona os relacionamentos com gêneros
            foreach (var generoId in dto.Generos ?? Enumerable.Empty<int>())
            {
                livro.AdicionarGenero(generoId);
            }

            await _livroRepository.AdicionarAsync(livro);
        }

        public async Task AtualizarAsync(LivroDto dto)
        {
            var livroExistente = await _livroRepository.ObterPorIdAsync(dto.Id);
            if (livroExistente == null) return;

            livroExistente.AtualizarTitulo(dto.Titulo);
            livroExistente.AtualizarAutor(dto.AutorId);
            livroExistente.AtualizarEditora(dto.EditoraId);

            livroExistente.LimparGeneros();

            foreach (var generoId in dto.Generos ?? Enumerable.Empty<int>())
            {
                livroExistente.AdicionarGenero(generoId);
            }

            await _livroRepository.AtualizarAsync(livroExistente);
        }

        public async Task RemoverAsync(int id)
        {
            await _livroRepository.RemoverAsync(id);
        }
    }
}
