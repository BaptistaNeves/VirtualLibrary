using FluentValidation;
using VirtualLibrary.Domain.Models;
using VirtualLibrary.Business.Validators.Helpers;

namespace VirtualLibrary.Business.Validators
{
    public class ClienteValidation : AbstractValidator<Cliente>
    {
        public ClienteValidation()
        {
            RuleFor(c => c.Nome)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(2, 255).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");
            
            RuleFor(c => c.Email)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .EmailAddress().WithMessage("Email Inválido!");
            
            RuleFor(c => c.Telefone)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(9).WithMessage("Número de Telefone inválido!");
            
            RuleFor(c => c.DataNascimento)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
                    
            When(c => c.DataNascimento != null, () => {
                RuleFor(f => CalculateAge.CompareAge(f.DataNascimento)).Equal(true)
                .WithMessage("O Fornecedor precisa ser Maior de idade!");
            });

            RuleFor(c => c.Endereco)
                     .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(2, 300).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLength} caracteres!");
            
            RuleFor(c => c.TipoDocumento)
                     .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido");
            
            RuleFor(c => c.NumeroDocumento)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido")
                    .Length(13).WithMessage("O campo {PropertyName} precisa ter 13 caracteres!");

        }
    }
}