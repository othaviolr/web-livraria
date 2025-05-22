using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<GeneroDto> ObterPorIdAsync(int id)
        {
            var genero = await _generoRepository.ObterPorIdAsync(id);
            if (genero == null) return null;

            return new GeneroDto
            {
                Id = genero.Id,
                Nome = genero.Nome
            };
        }

        public async Task<GeneroDto> AdicionarAsync(GeneroDto dto)
        {
            var genero = new Genero(dto.Nome);
            await _generoRepository.AdicionarAsync(genero);

            // Atualiza o Id do DTO com o Id gerado
            dto.Id = genero.Id;
            return dto;
        }

        public async Task AtualizarAsync(GeneroDto dto)
        {
            var generoExistente = await _generoRepository.ObterPorIdAsync(dto.Id);
            if (generoExistente == null) return;

            generoExistente.AtualizarNome(dto.Nome);
            await _generoRepository.AtualizarAsync(generoExistente);
        }

        public async Task RemoverAsync(int id)
        {
            await _generoRepository.RemoverAsync(id);
        }
    }
}
