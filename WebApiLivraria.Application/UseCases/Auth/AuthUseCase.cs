using Google.Apis.Auth;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Domain.Entities;
using Microsoft.Extensions.Configuration;
using WebApiLivraria.Application.Services;

namespace WebApiLivraria.Application.UseCases.Auth;

public class AuthUseCase : IAuthUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly ITokenService _tokenService;
    private readonly string _googleClientId;

    public AuthUseCase(IUsuarioRepository usuarioRepository, ITokenService tokenService, IConfiguration configuration)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
        _googleClientId = configuration["Authentication:Google:ClientId"]!;
    }

    public async Task<string> LoginComGoogle(LoginGoogleRequest request)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { _googleClientId }
        });

        var usuario = await _usuarioRepository.ObterPorEmail(payload.Email);
        if (usuario is null)
        {
            usuario = new Usuario(payload.Name, payload.Email);
            await _usuarioRepository.Adicionar(usuario);
        }

        return _tokenService.GerarToken(usuario);
    }
}