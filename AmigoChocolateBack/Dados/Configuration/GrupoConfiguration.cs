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
            builder.ToTable("GrupoAmigoChocolate", "dbo");
            //builder.ToTable("GrupoAmigoChocolate");
            builder.HasKey(g => g.IDGrupo);

                builder.Property(g => g.NomeGrupo)
                    .IsRequired()
                    .HasMaxLength(255);

            //builder.Property(g => g.Icone);

                builder.Property(g => g.QuantidadeMaxima)
                    .IsRequired();

                builder.Property(g => g.ValorChocolate)
                    .HasColumnType("decimal(10, 2)");

            builder.Property(g => g.DataRevelacao);
                   // .IsRequired();

                builder.Property(g => g.Descricao)
                    .HasColumnType("text");
            }
        }
    
}
