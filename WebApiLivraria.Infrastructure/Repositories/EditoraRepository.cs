using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Context;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class EditoraRepository : IEditoraRepository
    {
        private readonly AppDbContext _context;

        public EditoraRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Editora editora)
        {
            await _context.Editoras.AddAsync(editora);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Editora editora)
        {
            _context.Editoras.Update(editora);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var editora = await _context.Editoras.FindAsync(id);
            if (editora != null)
            {
                _context.Editoras.Remove(editora);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Editora> ObterPorIdAsync(int id)
        {
            return await _context.Editoras.FindAsync(id);
        }

        public async Task<IEnumerable<Editora>> ListarAsync()
        {
            return await _context.Editoras.ToListAsync();
        }
    }
}
