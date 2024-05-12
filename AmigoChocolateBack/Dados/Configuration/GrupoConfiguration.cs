using AmigoChocolateBack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Text.RegularExpressions;

namespace AmigoChocolateBack.Dados.Configuration
{
   

 
        public class GrupoConfiguration : IEntityTypeConfiguration<Grupo>
        {
            public void Configure(EntityTypeBuilder<Grupo> builder)
            {
                builder.ToTable("Grupos", "AppMobile");
                builder.HasKey(g => g.ID);

                builder.Property(g => g.Nome)
                    .IsRequired()
                    .HasMaxLength(255);

                builder.Property(g => g.Icone)
                    .IsRequired();

                builder.Property(g => g.QuantidadeMaxParticipantes)
                    .IsRequired();

                builder.Property(g => g.Valor)
                    .HasColumnType("decimal(10, 2)");

                builder.Property(g => g.DataRevelacao)
                    .IsRequired();

                builder.Property(g => g.Descricao)
                    .HasColumnType("text");
            }
        }
    
}
