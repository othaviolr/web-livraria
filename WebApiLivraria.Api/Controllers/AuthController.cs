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

    [HttpPut("perfil")]
    [Authorize]
    public async Task<IActionResult> AtualizarPerfil([FromBody] AtualizarPerfilRequest request)
    {
        try
        {
            var usuarioIdStr = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!Guid.TryParse(usuarioIdStr, out var usuarioId))
                return Unauthorized(new { message = "Usuário não autenticado." });

            var sucesso = await _atualizarPerfilUseCase.ExecutarAsync(usuarioId, request);

            if (!sucesso)
                return NotFound(new { message = "Usuário não encontrado." });

            return Ok(new { message = "Perfil atualizado com sucesso." });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = $"Erro ao atualizar perfil: {ex.Message}" });
        }
    }
}