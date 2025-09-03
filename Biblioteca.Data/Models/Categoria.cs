using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.Models
{
    public class Categoria
    {
        public int Id { get; set; } // chave primária
        [Required, MaxLength(50)]
        public string Nome { get; set; } = "";

        // Navegação: categoria pode ter vários livros
        public List<Livro> Livros { get; set; } = new();
    }
}
