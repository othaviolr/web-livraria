using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.UseCases.Auth;
using WebApiLivraria.Application.UseCases.Usuarios.Login;

namespace WebApiLivraria.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCase _authUseCase;
    private readonly LoginUsuarioUseCase _loginUsuarioUseCase;

    public AuthController(IAuthUseCase authUseCase, LoginUsuarioUseCase loginUsuarioUseCase)
    {
        _authUseCase = authUseCase;
        _loginUsuarioUseCase = loginUsuarioUseCase;
    }

    [HttpPost("login-google")]
    public async Task<IActionResult> LoginComGoogle([FromBody] LoginGoogleRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var token = await _authUseCase.LoginComGoogle(request);
            return Ok(new { Token = token });
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginTradicional([FromBody] LoginUsuarioRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var response = await _loginUsuarioUseCase.Executar(request);
            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { Message = ex.Message });
        }
    }
}