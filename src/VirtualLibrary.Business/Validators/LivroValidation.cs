using FluentValidation;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Business.Validators
{
    public class LivroValidation : AbstractValidator<Livro>
    {
        public LivroValidation()
        {
            RuleFor(l => l.Titulo)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                    .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLenght} caracteres!");

            RuleFor(l => l.Imagem)
                    .NotEmpty().WithMessage("O campo {PropertyName} precisa ser fornecido!")
                    .Length(2, 100).WithMessage("O campo {PropertyName} precisa ter entre {MinLength} e {MaxLenght} caracteres!");

            RuleFor(l => l.Descricao)
                    .MaximumLength(100).WithMessage("o campo {PropertyName} precisa ter no máximo 1000 caracteres!");
            
            RuleFor(l => l.CategoriaId)
                    .NotEmpty().WithMessage("O campo Categoria precisa ser fornecido!");

            RuleFor(l => l.AutorId)
                    .NotEmpty().WithMessage("O campo Autor precisa ser fornecido!");
            
            RuleFor(l => l.EditoraId)
                    .NotEmpty().WithMessage("O campo Editora precisa ser fornecido!");
            
            RuleFor(l => l.FornecedorId)
                    .NotEmpty().WithMessage("O campo Fornecedor precisa ser fornecido!");
        }
    }
}