using Microsoft.EntityFrameworkCore;
using WebMinimalOracle.Models;

namespace WebMinimalOracle.Config
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            //usar só em teste
            //Database.EnsureCreated();
        }

        public DbSet<Produto> Produto { get; set; }

    }
}
