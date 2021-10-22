using FluentValidation;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Business.Validators
{
    public class EditoraValidation : AbstractValidator<Editora>
    {
        public EditoraValidation()
        {
            RuleFor(e => e.Nome)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(e => e.Pais)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");

            RuleFor(e => e.Cidade)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");
            
            RuleFor(e => e.Descricao)
                    .MaximumLength(1000).WithMessage("o campo {PropertyName} precisa ter apenas 1000 caracteres!");
        }
    }
}