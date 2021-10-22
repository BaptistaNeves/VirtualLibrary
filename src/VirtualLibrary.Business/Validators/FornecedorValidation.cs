using FluentValidation;
using VirtualLibrary.Domain.Models;
using VirtualLibrary.Business.Validators.Helpers;

namespace VirtualLibrary.Business.Validators
{
    public class FornecedorValidation : AbstractValidator<Fornecedor>
    {
        public FornecedorValidation()
        {
            RuleFor(f => f.Nome)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");
            
            RuleFor(f => f.email)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .EmailAddress().WithMessage("Formato de Email Inválido!");
            
            RuleFor(f => f.Telefone)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(9).WithMessage("Número de Telefone inválido!");
            
            RuleFor(f => f.DataNascimento)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
                    
            When(f => f.DataNascimento != null, () => {
                RuleFor(f => CalculateAge.CompareAge(f.DataNascimento)).Equal(true)
                .WithMessage("O Fornecedor precisa ser Maior de idade!");
            });

            RuleFor(f => f.Endereco)
                     .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(2, 300).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");
            
            RuleFor(f => f.TipoDocumento)
                     .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            
            RuleFor(f => f.NumeroDocumento)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(13).WithMessage("O campo {PropertyName} precisa ter 13 caracteres!");

        }
    }
}