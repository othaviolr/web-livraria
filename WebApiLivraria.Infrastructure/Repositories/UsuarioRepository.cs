using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Enums;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Context;

namespace WebApiLivraria.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;

        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario?> ObterPorEmail(string email)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<Usuario?> ObterPorId(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Adicionar(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Usuario usuario)
        {
            _context.Usuarios.Update(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> ObterPorIdComAvaliacoesAsync(Guid id)
        {
            return await _context.Usuarios
                .Include(u => u.Avaliacoes)
                    .ThenInclude(a => a.Livro)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task Remover(Usuario usuario)
        {
            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<Usuario?> ObterPorNomeUsuarioComRelacionamentosAsync(string nomeUsuario)
        {
            return await _context.Usuarios
                .Include(u => u.Avaliacoes)
                    .ThenInclude(a => a.Livro)
                        .ThenInclude(l => l.Autor)  
                .Include(u => u.Favoritos)
                    .ThenInclude(f => f.Livro)
                        .ThenInclude(l => l.Autor)   
                .Include(u => u.LivrosLidos)
                    .ThenInclude(ll => ll.Livro)
                        .ThenInclude(l => l.Autor)  
                .FirstOrDefaultAsync(u => u.NomeUsuario == nomeUsuario);
        }

        public async Task<Usuario?> ObterPorIdComDetalhesAsync(Guid id)
        {
            return await _context.Usuarios
                .Include(u => u.Avaliacoes)
                    .ThenInclude(a => a.Livro)
                        .ThenInclude(l => l.Autor)
                .Include(u => u.Favoritos)
                    .ThenInclude(f => f.Livro)
                        .ThenInclude(l => l.Autor)
                .Include(u => u.LivrosLidos)
                    .ThenInclude(ll => ll.Livro)
                        .ThenInclude(l => l.Autor)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<Dictionary<StatusLeitura, int>> ObterContagemLivrosPorStatusAsync(Guid usuarioId)
        {
            var leituras = await _context.Leituras
                .Where(l => l.UsuarioId == usuarioId)
                .GroupBy(l => l.Status)
                .Select(g => new { Status = g.Key, Quantidade = g.Count() })
                .ToListAsync();

            var resultado = Enum.GetValues(typeof(StatusLeitura))
                .Cast<StatusLeitura>()
                .ToDictionary(status => status, status => 0);

            foreach (var item in leituras)
            {
                resultado[item.Status] = item.Quantidade;
            }

            return resultado;
        }

        public async Task<int> ObterQuantidadeFavoritosAsync(Guid usuarioId)
        {
            return await _context.Favoritos.CountAsync(f => f.UsuarioId == usuarioId);
        }

        public async Task<bool> ExistePorIdAsync(Guid id)
        {
            return await _context.Usuarios.AnyAsync(u => u.Id == id);
        }
    }
}