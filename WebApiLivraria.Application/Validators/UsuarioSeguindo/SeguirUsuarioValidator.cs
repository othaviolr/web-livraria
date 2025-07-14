using FluentValidation;
using WebApiLivraria.Application.Requests.UsuarioSeguindo;

namespace WebApiLivraria.Application.Validators.UsuarioSeguindo
{
    public class SeguirUsuarioValidator : AbstractValidator<SeguirUsuarioRequest>
    {
        public SeguirUsuarioValidator()
        {
            RuleFor(x => x.UsuarioIdParaSeguir)
                .NotEmpty().WithMessage("Informe o ID do Usuário que deseja seguir.");
        }
    }
}