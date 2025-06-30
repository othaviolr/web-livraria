namespace WebApiLivraria.Application.UseCases.Avaliacao.Editar
{
    public interface IEditarAvaliacaoUseCase
    {
        Task ExecutarAsync(EditarAvaliacaoRequest request);
    }
}