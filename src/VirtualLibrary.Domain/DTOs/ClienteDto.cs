using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using VirtualLibrary.Domain.DTOs.Usuario;

namespace VirtualLibrary.Domain.DTOs
{
    public class ClienteDto
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser fornecido!")]
        [MinLength(2, ErrorMessage = "O campo {0} dever no minimo {1} caracteres!")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser fornecido!")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string email { get; set; }

        [Required(ErrorMessage = "O campo {0} deve ser fornecido!")]
        [StringLength(9, ErrorMessage = "Número de telefone inválido!" , MinimumLength = 9)]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O endereço deve ser fornecido!")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Escolha o tipo de documento!")]
        public string TipoDocumento { get; set; }

        [Required(ErrorMessage = "O número do documento deve ser fornecido!")]
        public string NumeroDocumento { get; set; }

        [Required(ErrorMessage = "Informe a data de Nascimento!")]
        [DataType(DataType.DateTime)]
        public DateTime DataNascimento { get; set; }
        public int Idade { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataCadastro { get; set; }

        [DataType(DataType.DateTime)]
        public DateTime DataAtualizacao { get; set; }

        public UsuarioDto Usuario { get; set; }
        public IEnumerable<AluguerDto> Alugueres { get; set; }
    }
}