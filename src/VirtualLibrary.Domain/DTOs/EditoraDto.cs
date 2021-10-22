using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtualLibrary.Domain.DTOs.Usuario;

namespace VirtualLibrary.Domain.DTOs
{
    public class EditoraDto
    {

        [Key]
        public Guid Id { get; set; }
        
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser fornecido")]
        [MinLength(2, ErrorMessage = "O campo {0} deve ter no minimo {1} caracteres")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser fornecido!")]
        public string Pais { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser fornecido!")]
        public string Cidade { get; set; }

        [StringLength(1000, ErrorMessage = "O campo {0} deve ter no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataAtualizacao { get; set; }

        public UsuarioDto Usuario { get; set; }
        public IEnumerable<LivroDto> Livros { get; set; }
    }
}