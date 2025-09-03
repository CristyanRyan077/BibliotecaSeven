using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.Models
{
    public class Aluguel
    {
        public int Id { get; set; } // chave primária
        public DateTime DataLocacao { get; set; }
        public DateTime DataDevolucaoReal { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public int LivroId { get; set; } // FK_Livro_Aluguel
        public int UsuarioId { get; set; } // FK_Usuario_Aluguel

        public Usuario? Usuario { get; set; } // Propriedade de navegação para Usuario
        public Livro? Livro { get; set; } // Propriedade de navegação para Livro
    }
}
