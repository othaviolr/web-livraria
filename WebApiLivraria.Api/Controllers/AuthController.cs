using Microsoft.AspNetCore.Mvc;
using WebApiLivraria.Application.UseCases.Auth;
using WebApiLivraria.Application.UseCases.Usuarios.Login;
using WebApiLivraria.Application.UseCases.Usuarios.RegistrarUsuario;

namespace WebApiLivraria.Api.Controllers;

[ApiController]
[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCase _authUseCase;
    private readonly LoginUsuarioUseCase _loginUsuarioUseCase;
    private readonly RegistrarUsuarioUseCase _registrarUsuarioUseCase;

    public AuthController(
        IAuthUseCase authUseCase,
        LoginUsuarioUseCase loginUsuarioUseCase,
        RegistrarUsuarioUseCase registrarUsuarioUseCase)
    {
        _authUseCase = authUseCase;
        _loginUsuarioUseCase = loginUsuarioUseCase;
        _registrarUsuarioUseCase = registrarUsuarioUseCase;
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

    [HttpPost("registrar")]
    public async Task<IActionResult> Registrar([FromBody] RegistrarUsuarioRequest request)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        try
        {
            var usuarioId = await _registrarUsuarioUseCase.Executar(request);
            return CreatedAtAction(nameof(Registrar), new { id = usuarioId });
        }
        catch (Exception ex)
        {
            return BadRequest(new { Message = ex.Message });
        }
    }
}