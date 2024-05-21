using AmigoChocolateBack.Dados.Configuration;
using AmigoChocolateBack.Models;
using Microsoft.EntityFrameworkCore;

namespace AmigoChocolateBack.Dados
{
    public class AmigoChocolateBackContext : DbContext
    {
        public AmigoChocolateBackContext(DbContextOptions<AmigoChocolateBackContext> options) : base(options)
        {
        }

        public DbSet<Grupo> Grupos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GrupoConfiguration());
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data source = 10.107.176.41,1434;
                                    Database = BD043411; 
                                    User ID = RA043411; 
                                    Password = 043411;
                                    TrustServerCertificate=true");
        }
    }
}

//201.62.57.93, 1434 