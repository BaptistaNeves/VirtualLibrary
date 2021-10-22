using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtualLibrary.Domain.DTOs.Usuario;
using System.Security.Claims;

namespace VirtualLibrary.Domain.DTOs
{
    public class CategoriaDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} precisa ser informado")]
        [StringLength(255, ErrorMessage = "O campo {0} precisa ter {2} e {1} caracteres!", MinimumLength = 2)]
        public string Descricao { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Formato de Data Inválida!")]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.DateTime, ErrorMessage = "Formato de Data Inválida!")]
        public DateTime DataAtualizacao { get; set; }
        
        public Guid UsuarioId { get; set; }
        public UsuarioDto Usuario { get; set; }
        public IEnumerable<LivroDto> Livros { get; set; }
    }
}