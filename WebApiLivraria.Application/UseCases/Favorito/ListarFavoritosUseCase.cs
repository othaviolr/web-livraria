using WebApiLivraria.Domain.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiLivraria.Application.UseCases.Favorito
{
    public class ListarFavoritosUseCase : IListarFavoritosUseCase
    {
        private readonly IFavoritoRepository _favoritoRepository;

        public ListarFavoritosUseCase(IFavoritoRepository favoritoRepository)
        {
            _favoritoRepository = favoritoRepository;
        }

        public async Task<List<FavoritoResponse>> Executar(string usuarioId)
        {
            var favoritos = await _favoritoRepository.ListarPorUsuario(usuarioId);

            return favoritos.Select(f => new FavoritoResponse
            {
                LivroId = f.LivroId,
                DataCriacao = f.DataCriacao
            }).ToList();
        }

        public async Task<bool> VerificarFavorito(string usuarioId, string livroId)
        {
            return await _favoritoRepository.Existe(usuarioId, livroId);
        }
    }
}