namespace WebApiLivraria.Application.UseCases.Avaliacao.Resumo
{
    public interface IObterResumoAvaliacaoLivroUseCase
    {
        Task<ResumoAvaliacaoLivroResponse> ExecutarAsync(int livroId);
    }
}