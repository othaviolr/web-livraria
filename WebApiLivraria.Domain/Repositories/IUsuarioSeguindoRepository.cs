using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Repositories
{
    public interface IUsuarioSeguindoRepository
    {
        Task SeguirAsync(UsuarioSeguindo usuarioSeguindo);
        Task DeixarDeSeguirAsync(Guid seguidorId, Guid seguindoId);
        Task<bool> ExisteRelacionamentoAsync(Guid seguidorId, Guid seguindoId);
        Task<List<Usuario>> ObterSeguidoresAsync(Guid usuarioId);
        Task<List<Usuario>> ObterSeguindoAsync(Guid usuarioId);
    }
}