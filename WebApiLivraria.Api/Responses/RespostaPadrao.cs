public class RespostaPadrao<T>
{
    public bool Sucesso { get; set; }
    public string Mensagem { get; set; }
    public T Dados { get; set; }

    public static RespostaPadrao<T> ComSucesso(T dados, string mensagem = null)
    {
        return new RespostaPadrao<T> { Sucesso = true, Mensagem = mensagem, Dados = dados };
    }

    public static RespostaPadrao<T> ComErro(string mensagem)
    {
        return new RespostaPadrao<T> { Sucesso = false, Mensagem = mensagem, Dados = default };
    }
}
