namespace WebApiLivraria.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }

    protected Usuario() { }

    public Usuario(string nome, string email)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
    }
}