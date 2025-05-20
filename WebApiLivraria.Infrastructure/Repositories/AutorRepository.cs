using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Context;
using WebApiLivraria.Infrastructure.Data;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class AutorRepository : IAutorRepository
    {
        private readonly AppDbContext _context;

        public AutorRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Autor autor)
        {
            await _context.Autores.AddAsync(autor);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Autor autor)
        {
            _context.Autores.Update(autor);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor != null)
            {
                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Autor> ObterPorIdAsync(Guid id)
        {
            return await _context.Autores.FindAsync(id);
        }

        public async Task<IEnumerable<Autor>> ListarAsync()
        {
            return await _context.Autores.ToListAsync();
        }
    }
}
