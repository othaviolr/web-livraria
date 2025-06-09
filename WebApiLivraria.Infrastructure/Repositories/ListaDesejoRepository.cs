using Microsoft.EntityFrameworkCore;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Infrastructure.Context;

namespace WebApiLivraria.Infra.Data.Repositories;

public class ListaDesejoRepository : IListaDesejoRepository
{
    private readonly AppDbContext _context;

    public ListaDesejoRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task Adicionar(ListaDesejo listaDesejo)
    {
        _context.ListasDesejo.Add(listaDesejo);
        await _context.SaveChangesAsync();
    }

    public async Task Remover(Guid usuarioId, Guid livroId)
    {
        var item = await _context.ListasDesejo
            .FirstOrDefaultAsync(ld => ld.UsuarioId == usuarioId && ld.LivroId == livroId);

        if (item != null)
        {
            _context.ListasDesejo.Remove(item);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<bool> Existe(Guid usuarioId, Guid livroId)
    {
        return await _context.ListasDesejo
            .AnyAsync(ld => ld.UsuarioId == usuarioId && ld.LivroId == livroId);
    }

    public async Task<IReadOnlyCollection<ListaDesejo>> ListarPorUsuario(Guid usuarioId)
    {
        return await _context.ListasDesejo
            .Where(ld => ld.UsuarioId == usuarioId)
            .ToListAsync();
    }
}