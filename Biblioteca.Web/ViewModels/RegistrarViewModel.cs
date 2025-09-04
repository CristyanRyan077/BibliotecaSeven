using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.ViewModels
{
    public class RegistrarViewModel
    {
        [Required(ErrorMessage = "Usuário é obrigatório")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        public string Email { get; set; } = "";

        [Required(ErrorMessage = "Senha é obrigatória")]
        public string Password { get; set; } = "";

        [Required(ErrorMessage = "Telefone é obrigatório")]
        public string Telefone { get; set; } = "";

        public string? ErrorMessage { get; set; }
    }
}
