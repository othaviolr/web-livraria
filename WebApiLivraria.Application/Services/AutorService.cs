using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;

namespace WebApiLivraria.Application.Services
{
    public class AutorService : IAutorService
    {
        private readonly IAutorRepository _autorRepository;

        public AutorService(IAutorRepository autorRepository)
        {
            _autorRepository = autorRepository;
        }

        public async Task<IEnumerable<AutorDto>> ListarAsync()
        {
            var autores = await _autorRepository.ListarAsync();
            return autores.Select(a => new AutorDto
            {
                Id = a.Id,
                Nome = a.Nome
            });
        }

        public async Task<AutorDto> ObterPorIdAsync(int id)
        {
            var autor = await _autorRepository.ObterPorIdAsync(id);
            if (autor == null) return null;

            return new AutorDto
            {
                Id = autor.Id,
                Nome = autor.Nome
            };
        }

        public async Task AdicionarAsync(AutorDto dto)
        {
            var autor = new Autor(dto.Nome);
            await _autorRepository.AdicionarAsync(autor);
        }

        public async Task AtualizarAsync(AutorDto dto)
        {
            var autorExistente = await _autorRepository.ObterPorIdAsync(dto.Id);
            if (autorExistente == null) return;

            autorExistente.AtualizarNome(dto.Nome);
            await _autorRepository.AtualizarAsync(autorExistente);
        }

        public async Task RemoverAsync(int id)
        {
            await _autorRepository.RemoverAsync(id);
        }
    }
}
