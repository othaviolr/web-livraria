using WebApiLivraria.Domain.Interfaces;  
using WebApiLivraria.Domain.Entities;   
using WebApiLivraria.Infrastructure.Context;  
using Microsoft.EntityFrameworkCore;


namespace WebApiLivraria.Infrastructure.Repositories
{
    public class LivroRepository : ILivroRepository
    {
        private readonly AppDbContext _context;

        public LivroRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(Livro livro)
        {
            await _context.Livros.AddAsync(livro);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Livro livro)
        {
            _context.Livros.Update(livro);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var livro = await _context.Livros.FindAsync(id);
            if (livro != null)
            {
                _context.Livros.Remove(livro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Livro> ObterPorIdAsync(int id)
        {
            return await _context.Livros
                .Include(l => l.LivroGeneros)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Livro>> ListarAsync()
        {
            return await _context.Livros
                .Include(l => l.LivroGeneros)
                .ToListAsync();
        }
    }
}
