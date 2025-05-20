using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;

namespace WebApiLivraria.Application.Services
{
    public class GeneroService : IGeneroService
    {
        private readonly IGeneroRepository _generoRepository;

        public GeneroService(IGeneroRepository generoRepository)
        {
            _generoRepository = generoRepository;
        }

        public async Task<IEnumerable<GeneroDto>> ListarAsync()
        {
            var generos = await _generoRepository.ListarAsync();
            return generos.Select(g => new GeneroDto
            {
                Id = g.Id,
                Nome = g.Nome
            });
        }

        public async Task<GeneroDto> ObterPorIdAsync(Guid id)
        {
            var genero = await _generoRepository.ObterPorIdAsync(id);
            if (genero == null) return null;

            return new GeneroDto
            {
                Id = genero.Id,
                Nome = genero.Nome
            };
        }

        public async Task AdicionarAsync(GeneroDto dto)
        {
            var genero = new Genero(dto.Nome);
            await _generoRepository.AdicionarAsync(genero);
        }

        public async Task AtualizarAsync(GeneroDto dto)
        {
            var generoExistente = await _generoRepository.ObterPorIdAsync(dto.Id);
            if (generoExistente == null) return;

            generoExistente.AtualizarNome(dto.Nome);
            await _generoRepository.AtualizarAsync(generoExistente);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _generoRepository.RemoverAsync(id);
        }
    }
}
