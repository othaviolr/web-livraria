namespace WebApiLivraria.Domain.Exceptions;

public class JaExisteException : Exception
{
    public JaExisteException(string mensagem)
        : base(mensagem)
    {
    }
}
