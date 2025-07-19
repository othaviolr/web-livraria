using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Entities;
using WebApiLivraria.Domain.Enums;

namespace WebApiLivraria.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<Usuario?> ObterPorEmail(string email);
        Task<Usuario?> ObterPorId(string id);
        Task Adicionar(Usuario usuario);
        Task Atualizar(Usuario usuario);
        Task Remover(Usuario usuario);
        Task<Usuario?> ObterPorIdComAvaliacoesAsync(string id);
        Task<Usuario?> ObterPorNomeUsuarioComRelacionamentosAsync(string nomeUsuario);
        Task<Usuario?> ObterPorIdComDetalhesAsync(string id);

        Task<Dictionary<StatusLeitura, int>> ObterContagemLivrosPorStatusAsync(string usuarioId);

        Task<int> ObterQuantidadeFavoritosAsync(string usuarioId);
        Task<bool> ExistePorIdAsync(string id);
    }
}