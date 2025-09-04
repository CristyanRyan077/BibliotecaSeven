using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data.Models
{
    [Index(nameof(ISBN), IsUnique = true)] // torna o campo ISBN único
    public class Livro
    {
        public int Id { get; set; }

        [Required, MaxLength(100)]
        public string Titulo { get; set; } = "";

        [Required]
        [Column(TypeName = "varchar(20)")]
        public string ISBN { get; set; } = "";
        public string? ImagemUrl { get; set; }
        public DateTime DataPublicacao { get; set; }
        public int QuantidadeDisponivel { get; set; }
        [Required]
        public string Autor { get; set; } = "";

        // FK Categoria 
        public int? CategoriaId { get; set; }
        public Categoria? Categoria { get; set; }

        // Navegação: alugueis desse livro
        public List<Aluguel> Alugueis { get; set; } = new();
       

    }
}
