using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.Services;
using WebApiLivraria.Application.UseCases.RankingLivro;

[ApiController]
[Route("api/[controller]")]
public class RankingController : ControllerBase
{
    private readonly IRankingService _rankingService;

    public RankingController(IRankingService rankingService)
    {
        _rankingService = rankingService;
    }

    [HttpGet]
    public async Task<IActionResult> ObterRanking([FromQuery] RankingLivroRequest request)
    {
        var ranking = await _rankingService.ObterRankingAsync(request);
        return Ok(ranking);
    }
}
