//using AmigoChocolateBack.Dados.Configuration;
//using AmigoChocolateBack.Models;
//using Microsoft.EntityFrameworkCore;

//namespace AmigoChocolateBack.Dados
//{
//    public class AmigoChocolateBackContext : DbContext
//    {
//        public AmigoChocolateBackContext(DbContextOptions<AmigoChocolateBackContext> options) : base(options)
//        {
//        }

//        public DbSet<Grupo> Grupos { get; set; }

//        protected override void OnModelCreating(ModelBuilder modelBuilder)
//        {
//            modelBuilder.ApplyConfiguration(new GrupoConfiguration());
//        }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            optionsBuilder.UseSqlServer(@"Data source = 201.62.57.93, 1434;
//                                    Database = BD043411; 
//                                    User ID = RA043411; 
//                                    Password = 043411;
//                                    TrustServerCertificate=true");

//        }



//    }
//}

////201.62.57.93, 1434 
////10.107.176.41,1434
///
using AmigoChocolateBack.Dados.Configuration;
using AmigoChocolateBack.Models;
using AmigoChocolateBack.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AmigoChocolateBack.Dados
{
    public class AmigoChocolateBackContext : DbContext
    {
        public AmigoChocolateBackContext(DbContextOptions<AmigoChocolateBackContext> options) : base(options)
        {
        }

        public DbSet<Grupo> Grupos { get; set; }
        public DbSet<Participante> Participantes { get; set; }
        public DbSet<GrupoParticipante2> GrupoParticipante2 { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new GrupoConfiguration());
            modelBuilder.ApplyConfiguration(new ParticipanteConfiguration());
            modelBuilder.ApplyConfiguration(new GrupoParticipante2Configuration());
            //modelBuilder.ApplyConfiguration(new ParticipanteConfiguration());



        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data source=201.62.57.93,1434;
                                          Database=BD043411; 
                                          User ID=RA043411; 
                                          Password=043411;
                                          TrustServerCertificate=true", options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 10,
                    maxRetryDelay: TimeSpan.FromSeconds(30),
                    errorNumbersToAdd: null);
            });
        }
    }
}