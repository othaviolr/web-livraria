namespace WebApiLivraria.Application.UseCases.Avaliacao.Resumo
{
    public interface IObterResumoAvaliacaoLivroUseCase
    {
        Task<ResumoAvaliacaoLivroResponse> ExecutarAsync(string livroId);
    }
}