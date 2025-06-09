using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.Favorito;

public class AdicionarFavoritoUseCase : IAdicionarFavoritoUseCase
{
    private readonly IFavoritoRepository _favoritoRepository;

    public AdicionarFavoritoUseCase(IFavoritoRepository favoritoRepository)
    {
        _favoritoRepository = favoritoRepository;
    }

    public async Task Executar(AdicionarFavoritoRequest request)
    {
        bool jaExiste = await _favoritoRepository.Existe(request.UsuarioId, request.LivroId);
        if (jaExiste)
        {
            throw new InvalidOperationException("Este livro já está nos favoritos.");
        }

        var favorito = new Domain.Entities.Favorito(request.UsuarioId, request.LivroId);
        await _favoritoRepository.Adicionar(favorito);
    }
}