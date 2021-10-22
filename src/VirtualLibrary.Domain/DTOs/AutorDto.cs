using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtualLibrary.Domain.DTOs.Usuario;

namespace VirtualLibrary.Domain.DTOs
{
    public class AutorDto
    {
        [Key]
        public Guid Id { get; set; }
        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage ="O campo {0} deve ser informado!")]
        [StringLength(100, ErrorMessage ="O campo {0} deve ter entre {2} a {1} caracteres!"), MinLength(2)]
        public string Nome { get; set; }

        [Required(ErrorMessage ="O campo {0} deve ser informado!")]
        [StringLength(50, ErrorMessage ="O campo {0} deve ter entre {2} a {1} caracteres!"), MinLength(2)]
        public string Nacionalidade { get; set; }

        [StringLength(1000, ErrorMessage ="O campo {0} deve ter no máximo {1} caracteres!")]
        public string Descricao { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataAtualizacao { get; set; }

        public IEnumerable<LivroDto> Livros { get; set; }
        public UsuarioDto Usuario { get; set; }
    }
}