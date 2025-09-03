using Biblioteca.Data.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Biblioteca.Data
{
    public class ContextFactory : IDesignTimeDbContextFactory<Context>
    {
        public Context CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<Context>();
            optionsBuilder.UseSqlServer("Data Source=2857AL17;Initial Catalog=BibliotecaDB;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");

            return new Context(optionsBuilder.Options);
        }
    }
}
