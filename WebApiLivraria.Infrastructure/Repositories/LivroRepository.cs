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
                .Include(l => l.Autor)
                .Include(l => l.Editora)
                .Include(l => l.LivroGeneros)
                    .ThenInclude(lg => lg.Genero)
                .Include(l => l.Sinopse)
                .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Livro>> ListarAsync()
        {
            return await _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Editora)
                .Include(l => l.LivroGeneros)
                    .ThenInclude(lg => lg.Genero)
                .Include(l => l.Sinopse)
                .ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ListarComFiltroAsync(string? search)
        {
            var query = _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Editora)
                .Include(l => l.LivroGeneros)
                    .ThenInclude(lg => lg.Genero)
                .Include(l => l.Sinopse)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                var filtroLower = search.ToLower();
                query = query.Where(l =>
                    l.Titulo.ToLower().Contains(filtroLower) ||
                    l.Autor.Nome.ToLower().Contains(filtroLower) ||
                    l.Editora.Nome.ToLower().Contains(filtroLower)
                );
            }

            return await query.ToListAsync();
        }

        public async Task<IEnumerable<Livro>> ObterLivrosComFiltroAsync(int? anoPublicacao, string? genero)
        {
            var query = _context.Livros
                .Include(l => l.Autor)
                .Include(l => l.Editora)
                .Include(l => l.LivroGeneros)
                    .ThenInclude(lg => lg.Genero)
                .Include(l => l.Avaliacoes)
                .Include(l => l.Sinopse)
                .AsQueryable();

            if (anoPublicacao.HasValue)
            {
                query = query.Where(l => l.AnoPublicacao.Year == anoPublicacao.Value);
            }

            if (!string.IsNullOrEmpty(genero))
            {
                query = query.Where(l => l.LivroGeneros.Any(g => g.Genero.Nome == genero));
            }

            return await query.ToListAsync();
        }
    }
}