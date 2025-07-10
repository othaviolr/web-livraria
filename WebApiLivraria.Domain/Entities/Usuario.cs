using WebApiLivraria.Domain.Entities;

public class Usuario
{
    public Guid Id { get; private set; }
    public string Nome { get; private set; }
    public string Email { get; private set; }

    public string? NomeUsuario { get; private set; }
    public string? FotoUrl { get; private set; }
    public string? Cidade { get; private set; }
    public string? Bio { get; private set; }
    public string Role { get; private set; } = "Leitor";

    public string? SenhaHash { get; private set; }
    public string? TokenRecuperacaoSenha { get; private set; }
    public DateTime? DataExpiracaoTokenRecuperacaoSenha { get; private set; }

    public ICollection<Avaliacao> Avaliacoes { get; private set; } = new List<Avaliacao>();
    public ICollection<ListaDesejo> ListasDesejo { get; private set; } = new List<ListaDesejo>();
    public ICollection<Favorito> Favoritos { get; private set; } = new List<Favorito>();
    public ICollection<Leitura> LivrosLidos { get; private set; } = new List<Leitura>();

    protected Usuario() { }

    public Usuario(string nome, string email, string? senhaHash = null)
    {
        Id = Guid.NewGuid();
        Nome = nome;
        Email = email;
        SenhaHash = senhaHash;
        Role = "Leitor";
    }

    public void AtualizarPerfil(string nomeUsuario, string? fotoUrl, string? cidade, string role, string? bio)
    {
        NomeUsuario = nomeUsuario;
        FotoUrl = fotoUrl;
        Cidade = cidade;
        Role = role;
        Bio = bio;
    }

    public void DefinirSenha(string senhaHash)
    {
        SenhaHash = senhaHash;
    }

    public void DefinirTokenRecuperacaoSenha(string token, DateTime dataExpiracao)
    {
        TokenRecuperacaoSenha = token;
        DataExpiracaoTokenRecuperacaoSenha = dataExpiracao;
    }

    public void RedefinirSenha(string novaSenhaHash)
    {
        SenhaHash = novaSenhaHash;
        TokenRecuperacaoSenha = null;
        DataExpiracaoTokenRecuperacaoSenha = null;
    }
}