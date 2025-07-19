using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApiLivraria.Application.UseCases.Auth;
using WebApiLivraria.Application.UseCases.Usuarios;
using WebApiLivraria.Application.UseCases.Usuarios.AtualizarPerfil;
using WebApiLivraria.Application.UseCases.Usuarios.Login;
using WebApiLivraria.Application.UseCases.Usuarios.RegistrarUsuario;

namespace WebApiLivraria.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthUseCase _authUseCase;
    private readonly LoginUsuarioUseCase _loginUsuarioUseCase;
    private readonly RegistrarUsuarioUseCase _registrarUsuarioUseCase;
    private readonly AtualizarPerfilUseCase _atualizarPerfilUseCase;

    public AuthController(
        IAuthUseCase authUseCase,
        LoginUsuarioUseCase loginUsuarioUseCase,
        RegistrarUsuarioUseCase registrarUsuarioUseCase,
        AtualizarPerfilUseCase atualizarPerfilUseCase)
    {
        _authUseCase = authUseCase;
        _loginUsuarioUseCase = loginUsuarioUseCase;
        _registrarUsuarioUseCase = registrarUsuarioUseCase;
        _atualizarPerfilUseCase = atualizarPerfilUseCase;
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
        {
            var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
            return BadRequest(new { message = string.Join("; ", errors) });
        }

        try
        {
            var response = await _loginUsuarioUseCase.Executar(request);

            if (response == null || string.IsNullOrEmpty(response.Token))
            {
                return Unauthorized(new { message = "Email ou senha inválidos." });
            }

            return Ok(response);
        }
        catch (Exception ex)
        {
            return Unauthorized(new { message = ex.Message });
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

    [HttpPut("perfil")]
    [Authorize]
    public async Task<IActionResult> AtualizarPerfil([FromBody] AtualizarPerfilRequest request)
    {
        var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (string.IsNullOrEmpty(usuarioIdStr))
            return Unauthorized(new { message = "Usuário não autenticado." });

        var sucesso = await _atualizarPerfilUseCase.ExecutarAsync(usuarioIdStr, request);

        if (!sucesso)
            return NotFound(new { message = "Usuário não encontrado." });

        return Ok(new { message = "Perfil atualizado com sucesso." });
    }

    [HttpGet("claims")]
    [Authorize]
    public IActionResult ListarClaims()
    {
        var claims = User.Claims.Select(c => new { c.Type, c.Value }).ToList();
        return Ok(claims);
    }
}