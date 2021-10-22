using FluentValidation;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Business.Validators
{
    public class CategoriaValidation : AbstractValidator<Categoria>
    {
        public CategoriaValidation()
        {
            RuleFor(c => c.Descricao)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                    .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLenght} caracteres!");
        }
    }
}