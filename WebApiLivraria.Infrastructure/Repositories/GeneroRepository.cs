using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Context;

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

        public async Task RemoverAsync(int id)
        {
            var genero = await _context.Generos.FindAsync(id);
            if (genero != null)
            {
                _context.Generos.Remove(genero);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Genero> ObterPorIdAsync(int id)
        {
            return await _context.Generos.FindAsync(id);
        }

        public async Task<IEnumerable<Genero>> ListarAsync()
        {
            return await _context.Generos.ToListAsync();
        }

        public async Task<IEnumerable<Genero>> ListarPorIdsAsync(IEnumerable<int> ids)
        {
            return await _context.Generos
                .Where(g => ids.Contains(g.Id))
                .ToListAsync();
        }
    }
}
