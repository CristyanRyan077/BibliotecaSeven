using Biblioteca.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Biblioteca.Data.Database
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Livro> Livros { get; set; }

        public DbSet<Aluguel> Alugueis { get; set; }
       


    }
}
