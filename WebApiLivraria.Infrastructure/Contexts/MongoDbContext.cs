using Microsoft.Extensions.Options;
using MongoDB.Driver;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Infrastructure.Configurations;

namespace WebApiLivraria.Infrastructure.Contexts;

public class MongoDbContext
{
    private readonly IMongoDatabase _database;

    public MongoDbContext(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        _database = client.GetDatabase(settings.Value.DatabaseName);
    }

    public IMongoCollection<Livro> Livros => _database.GetCollection<Livro>("Livros");
    public IMongoCollection<Autor> Autores => _database.GetCollection<Autor>("Autores");
    public IMongoCollection<Editora> Editoras => _database.GetCollection<Editora>("Editoras");
    public IMongoCollection<Usuario> Usuarios => _database.GetCollection<Usuario>("Usuarios");
    public IMongoCollection<UsuarioSeguindo> UsuariosSeguindo => _database.GetCollection<UsuarioSeguindo>("UsuariosSeguindo");
    public IMongoCollection<Avaliacao> Avaliacoes => _database.GetCollection<Avaliacao>("Avaliacoes");
    public IMongoCollection<Favorito> Favoritos => _database.GetCollection<Favorito>("Favoritos");
    public IMongoCollection<ListaDesejo> ListasDesejo => _database.GetCollection<ListaDesejo>("ListasDesejo");
    public IMongoCollection<Genero> Generos => _database.GetCollection<Genero>("Generos");
    public IMongoCollection<Leitura> Leituras => _database.GetCollection<Leitura>("Leituras");
    public IMongoCollection<RankingLivro> RankingLivros => _database.GetCollection<RankingLivro>("RankingLivros");
    public IMongoCollection<LivroGenero> LivroGeneros => _database.GetCollection<LivroGenero>("LivroGeneros");
    public IMongoCollection<Sinopse> Sinopses => _database.GetCollection<Sinopse>("Sinopses");
}