using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Context;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class UsuarioSeguindoRepository : IUsuarioSeguindoRepository
    {
        private readonly AppDbContext _context;

        public UsuarioSeguindoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task SeguirAsync(UsuarioSeguindo usuarioSeguindo)
        {
            _context.UsuariosSeguindo.Add(usuarioSeguindo);
            await _context.SaveChangesAsync();
        }

        public async Task DeixarDeSeguirAsync(Guid seguidorId, Guid seguindoId)
        {
            var relacionamento = await _context.UsuariosSeguindo
                .FirstOrDefaultAsync(us => us.SeguidorId == seguidorId && us.SeguindoId == seguindoId);

            if (relacionamento != null)
            {
                _context.UsuariosSeguindo.Remove(relacionamento);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteRelacionamentoAsync(Guid seguidorId, Guid seguindoId)
        {
            return await _context.UsuariosSeguindo
                .AnyAsync(us => us.SeguidorId == seguidorId && us.SeguindoId == seguindoId);
        }

        public async Task<List<Usuario>> ObterSeguidoresAsync(Guid usuarioId)
        {
            return await _context.UsuariosSeguindo
                .Where(us => us.SeguindoId == usuarioId)
                .Select(us => us.Seguidor!)
                .ToListAsync();
        }

        public async Task<List<Usuario>> ObterSeguindoAsync(Guid usuarioId)
        {
            return await _context.UsuariosSeguindo
                .Where(us => us.SeguidorId == usuarioId)
                .Select(us => us.Seguindo!)
                .ToListAsync();
        }
    }
}