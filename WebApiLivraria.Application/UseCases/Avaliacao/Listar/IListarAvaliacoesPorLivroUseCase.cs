using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Listar
{
    public interface IListarAvaliacoesPorLivroUseCase
    {
        Task<List<AvaliacaoResponse>> ExecutarAsync(string livroId);
    }
}