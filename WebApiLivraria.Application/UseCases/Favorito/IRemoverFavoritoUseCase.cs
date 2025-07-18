namespace WebApiLivraria.Application.UseCases.Favorito
{
    public interface IRemoverFavoritoUseCase
    {
        Task Executar(string usuarioId, int livroId);
    }

    public interface IListarFavoritosUseCase
    {
        Task<List<FavoritoResponse>> Executar(string usuarioId);
        Task<bool> VerificarFavorito(string usuarioId, string livroId);
    }
}
