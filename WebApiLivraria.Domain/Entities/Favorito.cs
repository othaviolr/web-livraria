namespace WebApiLivraria.Domain.Entities;

public class Favorito
{
    public Guid Id { get; private set; }
    public Guid UsuarioId { get; private set; }
    public int LivroId { get; private set; }
    public DateTime DataCriacao { get; private set; }

    public Usuario Usuario { get; private set; }
    public Livro Livro { get; private set; }

    protected Favorito() { }

    public Favorito(Guid usuarioId, int livroId)
    {
        Id = Guid.NewGuid();
        UsuarioId = usuarioId;
        LivroId = livroId;
        DataCriacao = DateTime.UtcNow;
    }
}