
using FluentValidation;

namespace Core.Application.UseCases.CreateRegister
{
    public class CreateRegisterValidator: AbstractValidator<CreateRegisterCommand>
    {
        public CreateRegisterValidator() {
            RuleFor(x => x.Contact).NotEmpty().WithMessage("El contacto es obligatorio");
            RuleFor(x => x.CompanyName).NotEmpty().WithMessage("El nombre de Compañia es obligatorio");
            RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Email invalido");
            RuleFor(x => x.Phone)
            .NotEmpty().WithMessage("El número de teléfono es obligatorio.")
            .Matches(@"^\+?[1-9]\d{1,14}$").WithMessage("El número de teléfono no es válido.");
        }
    
    }
}
