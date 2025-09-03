using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.Models
{
    public class Autor
    {
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Nome { get; set; } = "";

        [Required, MaxLength(50)]
        public string Sobrenome { get; set; } = "";

        // Navegação: um autor pode ter vários livros
        public List<Livro> Livros { get; set; } = new();
    }
}
