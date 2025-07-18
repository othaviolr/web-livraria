using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.Favorito;

public class RemoverFavoritoUseCase
{
    private readonly IFavoritoRepository _favoritoRepository;

    public RemoverFavoritoUseCase(IFavoritoRepository favoritoRepository)
    {
        _favoritoRepository = favoritoRepository;
    }

    public async Task Executar(Guid usuarioId, int livroId)
    {
        await _favoritoRepository.Remover(usuarioId.ToString(), livroId.ToString());
    }
}