using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Context;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class AvaliacaoRepository : IAvaliacaoRepository
    {
        private readonly AppDbContext _context;

        public AvaliacaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Avaliacao>> ListarPorLivroIdAsync(int livroId)
        {
            return await _context.Avaliacoes
                .AsNoTracking()
                .Where(a => a.LivroId == livroId)
                .ToListAsync();
        }

        public async Task AdicionarAsync(Avaliacao avaliacao)
        {
            await _context.Avaliacoes.AddAsync(avaliacao);
            await _context.SaveChangesAsync();
        }

        public async Task AtualizarAsync(Avaliacao avaliacao)
        {
            _context.Avaliacoes.Update(avaliacao);
            await _context.SaveChangesAsync();
        }

        public async Task RemoverAsync(int id)
        {
            var avaliacao = await _context.Avaliacoes.FindAsync(id);
            if (avaliacao != null)
            {
                _context.Avaliacoes.Remove(avaliacao);
                await _context.SaveChangesAsync();
            }
        }
    }
}
