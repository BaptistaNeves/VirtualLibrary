using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace VirtualLibrary.Domain.DTOs
{
    public class LivroAtualizarDto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "Informe a Categoria!")]
        public Guid CategoriaId { get; set; }

        [Required(ErrorMessage = "Informe o Autor!")]
        public Guid AutorId { get; set; }

        [Required(ErrorMessage = "Informe a Editora!")]
        public Guid EditoraId { get; set; }

        [Required(ErrorMessage = "Informe o Fornecedor!")]
        public Guid FornecedorId { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatório!")]
        public string Titulo { get; set; }

        public IFormFile ImagemLivro { get; set; }

        public string Imagem { get; set; }


        [StringLength(1000, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        public bool Estado { get; set; }
        

        [DataType(DataType.DateTime)]
        public DateTime DataAtualizacao { get; set; }

        public CategoriaDto Categoria { get; set; }
        public AutorDto Autor { get; set; }
        public EditoraDto Editora { get; set; }
        public FornecedorDto Fornecedor { get; set; }
    }
}