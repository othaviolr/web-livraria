using WebApiLivraria.Domain.Entities;

namespace WebApiLivraria.Application.Services
{
    public interface ITokenService
    {
        string GerarToken(Usuario usuario);
    }
}