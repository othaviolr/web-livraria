using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task RemoverAsync(int id)
        {
            var autor = await _context.Autores.FindAsync(id);
            if (autor != null)
            {
                _context.Autores.Remove(autor);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Autor> ObterPorIdAsync(int id)
        {
            return await _context.Autores.FindAsync(id);
        }

        public async Task<IEnumerable<Autor>> ListarAsync(string filtro = null)
        {
            var query = _context.Autores.AsQueryable();

            if (!string.IsNullOrEmpty(filtro))
            {
                filtro = filtro.ToLower();
                query = query.Where(a => a.Nome.ToLower().Contains(filtro));
            }

            return await query.ToListAsync();
        }

        public async Task<int> ObterMaiorIdAsync()
        {
            return await _context.Autores.MaxAsync(a => (int?)a.Id) ?? 0;
        }
    }
}