using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Application.Interfaces;
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

        public async Task<IEnumerable<Autor>> ListarAsync(string filtro = null, int? editoraId = null)
        {
            var query = _context.Autores.AsQueryable();

            if (!string.IsNullOrWhiteSpace(filtro))
            {
                var filtroLower = filtro.ToLower();
                query = query.Where(a => a.Nome.ToLower().Contains(filtroLower));
            }

            if (editoraId.HasValue)
            {
                query = query.Where(a => a.EditoraId == editoraId.Value);
            }

            return await query.ToListAsync();
        }

        public async Task<Autor> ObterPorIdAsync(int id)
        {
            return await _context.Autores
                .Include(a => a.Livros)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Autor> AdicionarAsync(Autor autor)
        {
            _context.Autores.Add(autor);
            await _context.SaveChangesAsync();
            return autor;
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
    }
}
