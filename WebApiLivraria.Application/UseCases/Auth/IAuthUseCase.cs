namespace WebApiLivraria.Application.UseCases.Auth;

public interface IAuthUseCase
{
    Task<string> LoginComGoogle(LoginGoogleRequest request);
}