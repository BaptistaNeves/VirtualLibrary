using System.ComponentModel.DataAnnotations;

namespace VirtualLibrary.Domain.DTOs.Usuario
{
    public class UsuarioEditDto
    {
        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "O campo {0} é Obrigatório")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "Informe o tipo de úsuario")]
        public string Role { get; set; }
        

    }
}