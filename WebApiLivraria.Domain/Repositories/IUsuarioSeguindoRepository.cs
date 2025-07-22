using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Domain.Repositories
{
    public interface IUsuarioSeguindoRepository
    {
        Task SeguirAsync(UsuarioSeguindo usuarioSeguindo);
        Task DeixarDeSeguirAsync(string seguidorId, string seguindoId);
        Task<bool> ExisteRelacionamentoAsync(string seguidorId, string seguindoId);

        Task<List<string>> ObterSeguidoresIdsAsync(string usuarioId);
        Task<List<string>> ObterSeguindoIdsAsync(string usuarioId);

        Task<List<Usuario>> ObterSeguidoresAsync(string usuarioId);
        Task<List<Usuario>> ObterSeguindoAsync(string usuarioId);
    }
}