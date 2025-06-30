namespace WebApiLivraria.Application.UseCases.Avaliacao.Excluir
{
    public interface IExcluirAvaliacaoUseCase
    {
        Task ExecutarAsync(int id, Guid usuarioId);
    }
}