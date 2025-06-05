using Google.Apis.Auth;
using WebApiLivraria.Domain.Repositories;
using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Application.UseCases.Auth;

public class AuthUseCase : IAuthUseCase
{
    private readonly IUsuarioRepository _usuarioRepository;
    private const string GoogleClientId = "659502960574-f6rqmigm8569v3gmaa77igr8of7rav6r.apps.googleusercontent.com";

    public AuthUseCase(IUsuarioRepository usuarioRepository)
    {
        _usuarioRepository = usuarioRepository;
    }

    public async Task<string> LoginComGoogle(LoginGoogleRequest request)
    {
        var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken, new GoogleJsonWebSignature.ValidationSettings
        {
            Audience = new[] { GoogleClientId }
        });

        var usuario = await _usuarioRepository.ObterPorEmail(payload.Email);
        if (usuario is null)
        {
            usuario = new Usuario(payload.Name, payload.Email);
            await _usuarioRepository.Adicionar(usuario);
        }

        return $"jwt-fake-para-{payload.Email}";
    }
}