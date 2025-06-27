using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
            return autores.Select(a => MapToDto(a));
        }

        public async Task<List<AutorDto>> ObterTodosAsync(string filtro = null)
        {
            var autores = await _autorRepository.ListarAsync(filtro);
            return autores.Select(a => MapToDto(a)).ToList();
        }

        public async Task<AutorDto> ObterPorIdAsync(int id)
        {
            var autor = await _autorRepository.ObterPorIdAsync(id);
            if (autor == null) return null;
            return MapToDto(autor);
        }

        public async Task<AutorDto> AdicionarAsync(AutorDto dto)
        {
            var ultimoId = await _autorRepository.ObterMaiorIdAsync();
            int novoId = ultimoId + 1;

            var autor = new Autor(
                dto.Nome,
                dto.EditoraId,
                dto.Biografia,
                dto.DataNascimento,
                dto.LocalNascimento,
                dto.FotoUrl
            );

            autor.SetId(novoId);

            await _autorRepository.AdicionarAsync(autor);

            return MapToDto(autor);
        }

        public async Task AtualizarAsync(AutorDto dto)
        {
            var autorExistente = await _autorRepository.ObterPorIdAsync(dto.Id);
            if (autorExistente == null) return;

            autorExistente.Atualizar(
                dto.Nome,
                dto.EditoraId,
                dto.Biografia,
                dto.DataNascimento,
                dto.LocalNascimento,
                dto.FotoUrl
            );

            await _autorRepository.AtualizarAsync(autorExistente);
        }

        public async Task RemoverAsync(int id)
        {
            await _autorRepository.RemoverAsync(id);
        }

        private AutorDto MapToDto(Autor autor)
        {
            return new AutorDto
            {
                Id = autor.Id,
                Nome = autor.Nome,
                EditoraId = autor.EditoraId,
                Biografia = autor.Biografia,
                DataNascimento = autor.DataNascimento,
                LocalNascimento = autor.LocalNascimento,
                FotoUrl = autor.FotoUrl
            };
        }
    }
}