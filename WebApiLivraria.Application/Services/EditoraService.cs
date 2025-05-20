using WebApiLivraria.Application.Dto;
using WebApiLivraria.Application.Interfaces;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;

namespace WebApiLivraria.Application.Services
{
    public class EditoraService : IEditoraService
    {
        private readonly IEditoraRepository _editoraRepository;

        public EditoraService(IEditoraRepository editoraRepository)
        {
            _editoraRepository = editoraRepository;
        }

        public async Task<IEnumerable<EditoraDto>> ListarAsync()
        {
            var editoras = await _editoraRepository.ListarAsync();
            return editoras.Select(e => new EditoraDto
            {
                Id = e.Id,
                Nome = e.Nome
            });
        }

        public async Task<EditoraDto> ObterPorIdAsync(Guid id)
        {
            var editora = await _editoraRepository.ObterPorIdAsync(id);
            if (editora == null) return null;

            return new EditoraDto
            {
                Id = editora.Id,
                Nome = editora.Nome
            };
        }

        public async Task AdicionarAsync(EditoraDto dto)
        {
            var editora = new Editora(dto.Nome);
            await _editoraRepository.AdicionarAsync(editora);
        }

        public async Task AtualizarAsync(EditoraDto dto)
        {
            var editoraExistente = await _editoraRepository.ObterPorIdAsync(dto.Id);
            if (editoraExistente == null) return;

            editoraExistente.AtualizarNome(dto.Nome);
            await _editoraRepository.AtualizarAsync(editoraExistente);
        }

        public async Task RemoverAsync(Guid id)
        {
            await _editoraRepository.RemoverAsync(id);
        }
    }
}
