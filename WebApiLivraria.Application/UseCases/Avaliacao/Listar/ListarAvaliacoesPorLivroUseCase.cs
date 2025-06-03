using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiLivraria.Domain.Interfaces;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Listar
{
    public class ListarAvaliacoesPorLivroUseCase : IListarAvaliacoesPorLivroUseCase
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public ListarAvaliacoesPorLivroUseCase(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task<List<AvaliacaoResponse>> ExecutarAsync(int livroId)
        {
            var avaliacoes = await _avaliacaoRepository.ListarPorLivroIdAsync(livroId);

            return avaliacoes.Select(a => new AvaliacaoResponse
            {
                Id = a.Id,
                LivroId = a.LivroId,
                UsuarioId = a.UsuarioId,
                Nota = a.Nota,
                Comentario = a.Comentario,
                DataAvaliacao = a.DataAvaliacao
            }).ToList();
        }
    }
}
