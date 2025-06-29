namespace WebApiLivraria.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }

    public string? NomeUsuario { get; private set; }
    public string? FotoUrl { get; private set; }
    public string? Cidade { get; private set; }
    public string Role { get; private set; } = "Leitor";

    protected Usuario() { }

    public Usuario(string nome, string email)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        Role = "Leitor";
    }

    public void AtualizarPerfil(string nomeUsuario, string? fotoUrl, string? cidade, string role)
    {
        NomeUsuario = nomeUsuario;
        FotoUrl = fotoUrl;
        Cidade = cidade;
        Role = role;
    }
}