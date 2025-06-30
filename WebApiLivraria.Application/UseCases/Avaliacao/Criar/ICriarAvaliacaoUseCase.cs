using System.Threading.Tasks;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Criar
{
    public interface ICriarAvaliacaoUseCase
    {
        Task ExecutarAsync(CriarAvaliacaoRequest request);
    }
}