using MongoDB.Driver;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories;

public class LivroGeneroRepository : ILivroGeneroRepository
{
    private readonly IMongoCollection<LivroGenero> _livroGeneros;

    public LivroGeneroRepository(MongoDbContext context)
    {
        _livroGeneros = context.LivroGeneros;
    }

    public async Task AtualizarGenerosDoLivroAsync(string livroId, IEnumerable<string> generoIds)
    {
        await _livroGeneros.DeleteManyAsync(lg => lg.LivroId == livroId);

        if (generoIds != null)
        {
            var novosLivroGeneros = new List<LivroGenero>();
            foreach (var generoId in generoIds)
            {
                novosLivroGeneros.Add(new LivroGenero(livroId, generoId));
            }
            if (novosLivroGeneros.Count > 0)
            {
                await _livroGeneros.InsertManyAsync(novosLivroGeneros);
            }
        }
    }

    public async Task<List<LivroGenero>> ObterPorLivroIdAsync(string livroId)
    {
        return await _livroGeneros.Find(lg => lg.LivroId == livroId).ToListAsync();
    }

    public async Task RemoverPorLivroIdAsync(string livroId)
    {
        await _livroGeneros.DeleteManyAsync(lg => lg.LivroId == livroId);
    }
}