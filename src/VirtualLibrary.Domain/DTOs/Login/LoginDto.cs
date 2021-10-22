using System.ComponentModel.DataAnnotations;

namespace VirtualLibrary.Domain.DTOs.Login
{
    public class LoginDto
    {
        [Required(ErrorMessage = "O campo {0} precisa ser fornecido!")]
        [EmailAddress(ErrorMessage = "Email inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo {0} precisa ser fornecido!")]
        public string Password { get; set; }
    }
}