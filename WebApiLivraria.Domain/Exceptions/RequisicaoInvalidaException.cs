namespace WebApiLivraria.Domain.Exceptions;

public class RequisicaoInvalidaException : Exception
{
    public RequisicaoInvalidaException(string mensagem)
        : base(mensagem)
    {
    }
}
