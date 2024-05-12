using AmigoChocolateBack.Dados.Configuration;
//using AmigoChocolateBack.Data.Configurations;
using AmigoChocolateBack.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Data.Entity;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace AmigoChocolateBack.Dados
{
   

   
        public class AmigoChocolateBackContext : DbContext
        {
           
            public Microsoft.EntityFrameworkCore.DbSet<Grupo> Grupos { get; set; }
            
            public AmigoChocolateBackContext(DbContextOptions<AmigoChocolateBackContext> options) : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
            {
                
                modelBuilder.ApplyConfiguration(new GrupoConfiguration());
                
            }
        }
    
}
