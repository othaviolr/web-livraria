using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.UseCases.Auth;

namespace WebApiLivraria.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCase _authUseCase;

    public AuthController(IAuthUseCase authUseCase)
    {
        _authUseCase = authUseCase;
    }

    [HttpPost("login-google")]
    public async Task<IActionResult> LoginComGoogle([FromBody] LoginGoogleRequest request)
    {
        var token = await _authUseCase.LoginComGoogle(request);
        return Ok(new { Token = token });
    }
}