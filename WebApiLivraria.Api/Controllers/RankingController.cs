using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.UseCases.RankingLivro;
using WebApiLivraria.Application.Dto;

namespace WebApiLivraria.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class RankingController : ControllerBase
    {
        private readonly IRankingLivroUseCase _rankingLivroUseCase;

        public RankingController(IRankingLivroUseCase rankingLivroUseCase)
        {
            _rankingLivroUseCase = rankingLivroUseCase;
        }

        [HttpGet]
        public async Task<IActionResult> ObterRanking([FromQuery] RankingLivroRequest request)
        {
            var resultado = await _rankingLivroUseCase.ExecutarAsync(request);

            return Ok(RespostaPadrao<List<RankingLivroResponse>>.ComSucesso(
                resultado,
                "Ranking obtido com sucesso."
            ));
        }
    }
}