using FluentValidation;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Business.Validators
{
    public class AutorValidation : AbstractValidator<Autor>
    {
        public AutorValidation()
        {
            RuleFor(a => a.Nome)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(a => a.Descricao)
                    .MaximumLength(1000).WithMessage("o campo {PropertyName} precisa ter apenas 1000 caracteres!");
            
            RuleFor(a => a.Nacionalidade)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(2, 50).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

        }
    }
}