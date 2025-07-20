using MongoDB.Driver;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Infrastructure.Contexts;

namespace WebApiLivraria.Infrastructure.Repositories;

public class SinopseRepository : ISinopseRepository
{
    private readonly IMongoCollection<Sinopse> _sinopses;

    public SinopseRepository(MongoDbContext context)
    {
        _sinopses = context.Sinopses;
    }

    public async Task<Sinopse?> ObterPorLivroIdAsync(string livroId)
    {
        return await _sinopses.Find(s => s.LivroId == livroId).FirstOrDefaultAsync();
    }

    public async Task AdicionarAsync(Sinopse sinopse)
    {
        await _sinopses.InsertOneAsync(sinopse);
    }

    public async Task AtualizarAsync(Sinopse sinopse)
    {
        await _sinopses.ReplaceOneAsync(s => s.LivroId == sinopse.LivroId, sinopse, new ReplaceOptions { IsUpsert = true });
    }

    public async Task RemoverPorLivroIdAsync(string livroId)
    {
        await _sinopses.DeleteOneAsync(s => s.LivroId == livroId);
    }

    public async Task<IEnumerable<Sinopse>> ListarPorLivroIdsAsync(IEnumerable<string> livroIds)
    {
        var filter = Builders<Sinopse>.Filter.In(s => s.LivroId, livroIds);
        return await _sinopses.Find(filter).ToListAsync();
    }
}