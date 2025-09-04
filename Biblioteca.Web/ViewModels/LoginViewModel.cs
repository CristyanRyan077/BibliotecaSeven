using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.ViewModels
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Nome é obrigatório")]
        public string Username { get; set; } = "";

        [Required(ErrorMessage = "Senha é obrigatório")]
        public string Password { get; set; } = "";
        public string? ErrorMessage { get; set; }
    }
}
