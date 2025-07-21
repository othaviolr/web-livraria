namespace WebApiLivraria.Application.UseCases.Avaliacao.Excluir
{
    public interface IExcluirAvaliacaoUseCase
    {
        Task ExecutarAsync(string id, string usuarioId);
    }
}