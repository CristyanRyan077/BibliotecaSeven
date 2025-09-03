using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.Models
{
    public class Usuario
    {
        public int Id { get; set; }  // chave primária

        [Required]
        [Column(TypeName = "Nvarchar(50)")]
        public string Username { get; set; } = "";

        [Required]
        [Column(TypeName = "Nvarchar(20)")]
        public string Telefone { get; set; } = "";
        [Required]
        [Column(TypeName = "Nvarchar(50)")]
        public string Email { get; set; } = ""; 
        public string PasswordHash { get; set; } = ""; // senha armazenada como hash

        public List<Aluguel> Alugueis { get; set; } = new();
    }
}
