using WebApiLivraria.Domain.Repositories;

namespace WebApiLivraria.Application.UseCases.Favorito
{
    public class RemoverFavoritoUseCase : IRemoverFavoritoUseCase
    {
        private readonly IFavoritoRepository _favoritoRepository;

        public RemoverFavoritoUseCase(IFavoritoRepository favoritoRepository)
        {
            _favoritoRepository = favoritoRepository;
        }

        public async Task Executar(string usuarioId, string livroId)
        {
            await _favoritoRepository.Remover(usuarioId, livroId);
        }
    }
}