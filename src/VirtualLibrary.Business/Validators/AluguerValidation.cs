using FluentValidation;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Business.Validators
{
    public class AluguerValidation : AbstractValidator<Aluguer>
    {
        public AluguerValidation()
        {
            RuleFor(a => a.PrecoUnitario)
                    .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
            
            RuleFor(a => a.PrecoTotal)
                    .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");
            
            RuleFor(a => a.Quantidade)
                    .GreaterThan(0).WithMessage("O campo {PropertyName} precisa ser maior que {ComparisonValue}");

            RuleFor(a => a.ClienteId)
                    .NotEmpty().WithMessage("O campo Categoria precisa ser fornecido!");

            RuleFor(a => a.LivroId)
                    .NotEmpty().WithMessage("O campo Categoria precisa ser fornecido!");
        }
    }
}