using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Context;

namespace WebApiLivraria.Infrastructure.Repositories;

public class FavoritoRepository : IFavoritoRepository
{
    private readonly AppDbContext _context;

    public FavoritoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Adicionar(Favorito favorito)
    {
        _context.Favoritos.Add(favorito);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(Guid usuarioId, int livroId)
    {
        var favorito = await _context.Favoritos
            .FirstOrDefaultAsync(f => f.UsuarioId == usuarioId && f.LivroId == livroId);

        if (favorito is not null)
        {
            _context.Favoritos.Remove(favorito);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> Existe(Guid usuarioId, int livroId)
    {
        return await _context.Favoritos
            .AnyAsync(f => f.UsuarioId == usuarioId && f.LivroId == livroId);
    }

    public async Task<IReadOnlyCollection<Favorito>> ListarPorUsuario(Guid usuarioId)
    {
        return await _context.Favoritos
            .Where(f => f.UsuarioId == usuarioId)
            .ToListAsync();
    }
}