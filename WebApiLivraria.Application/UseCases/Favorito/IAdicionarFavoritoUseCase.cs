namespace WebApiLivraria.Application.UseCases.Favorito;

public interface IAdicionarFavoritoUseCase
{
    Task Executar(AdicionarFavoritoRequest request);
}