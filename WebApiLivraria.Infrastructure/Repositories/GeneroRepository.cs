using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Context;
using WebApiLivraria.Infrastructure.Data;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class GeneroRepository : IGeneroRepository
    {
        private readonly AppDbContext _context;

        public GeneroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Genero genero)
        {
            await _context.Generos.AddAsync(genero);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Genero genero)
        {
            _context.Generos.Update(genero);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(Guid id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero != null)
            {
                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Genero> ObterPorIdAsync(Guid id)
        {
            return await _context.Generos.FindAsync(id);
        }

        public async Task<IEnumerable<Genero>> ListarAsync()
        {
            return await _context.Generos.ToListAsync();
        }
    }
}
