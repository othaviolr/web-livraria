using FluentValidation;

namespace WebApiLivraria.Application.UseCases.Usuarios.RegistrarUsuario
{
    public class RegistrarUsuarioValidator : AbstractValidator<RegistrarUsuarioRequest>
    {
        public RegistrarUsuarioValidator()
        {
            RuleFor(x => x.Nome).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Email).NotEmpty().EmailAddress();
            RuleFor(x => x.Senha).NotEmpty().MinimumLength(6);
        }
    }
}