using System.Threading.Tasks;

namespace WebApiLivraria.Application.UseCases.ListaDesejo
{
    public interface IAdicionarListaDesejoUseCase
    {
        Task Executar(AdicionarListaDesejoRequest request);
    }
}