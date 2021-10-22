using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VirtualLibrary.Domain.DTOs.Usuario
{
    public class UsuarioDto
    {
        [Key]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        [EmailAddress(ErrorMessage = "Email Inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        public string Endereco { get; set; }

        // [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        [StringLength(100, ErrorMessage = "O campo {0} precisa ter no minimo 6 carateres!", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Informe o tipo de úsuario")]
        public string Role { get; set; }
        
        public IEnumerable<string> Roles { get; set; }
    }
}