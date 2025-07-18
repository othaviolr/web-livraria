using System.Threading.Tasks;
using WebApiLivraria.Domain.Interfaces;
using WebApiLivraria.Domain.Entities;
using System;

namespace WebApiLivraria.Application.UseCases.Avaliacao.Criar
{
    public class CriarAvaliacaoUseCase : ICriarAvaliacaoUseCase
    {
        private readonly IAvaliacaoRepository _avaliacaoRepository;

        public CriarAvaliacaoUseCase(IAvaliacaoRepository avaliacaoRepository)
        {
            _avaliacaoRepository = avaliacaoRepository;
        }

        public async Task ExecutarAsync(CriarAvaliacaoRequest request)
        {
            var avaliacao = new WebApiLivraria.Domain.Entities.Avaliacao(
                livroId: request.LivroId.ToString(),         
                usuarioId: request.UsuarioId.ToString(),     
                nota: request.Nota,
                comentario: request.Comentario
            );

            await _avaliacaoRepository.AdicionarAsync(avaliacao);
        }
    }
}