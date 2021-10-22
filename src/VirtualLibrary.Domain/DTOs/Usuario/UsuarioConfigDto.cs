using System.ComponentModel.DataAnnotations;

namespace VirtualLibrary.Domain.DTOs.Usuario
{
    public class UsuarioConfigDto
    {
        [Required(ErrorMessage = "O campo {0} é Obrigatório!")]
        [EmailAddress(ErrorMessage = "Email Inválido!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Senha é Obrigatório!")]
        [StringLength(100, ErrorMessage = "O campo Senha precisa ter no minimo 6 carateres!", MinimumLength = 6)]
        public string Password { get; set; }

        [Required(ErrorMessage = "O campo Senha actual é Obrigatório!")]
        [StringLength(100, ErrorMessage = "O campo Senha actual precisa ter no minimo 6 carateres!", MinimumLength = 6)]
        public string CurrentPassword { get; set; }
    }
}