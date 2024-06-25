using AmigoChocolateBack.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmigoChocolateBack.Dados.Configuration
{
    public class ParticipanteConfiguration : IEntityTypeConfiguration<Participante>
    {
        public void Configure(EntityTypeBuilder<Participante> builder)
        {
            builder.ToTable("Participante", "dbo");
            builder.HasKey(p => p.IDParticipante);

            builder.Property(p => p.NomeParticipante)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.EmailParticipante)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.SenhaParticipante)
                .IsRequired()
                .HasMaxLength(100);

            //Configuração do relacionamento com Grupo
            //builder.HasMany(p => p.Grupos)
            //       .WithMany(g => g.Participantes)
            //       .UsingEntity<Dictionary<string, object>>(
            //           "ParticipanteGrupo",
            //           j => j.HasOne<Grupo>().WithMany().HasForeignKey("IDGrupo"),
            //           j => j.HasOne<Participante>().WithMany().HasForeignKey("IDParticipante"),
            //           j =>
            //           {
            //               j.HasKey("IDParticipante", "IDGrupo");
            //               j.ToTable("ParticipanteGrupo", "dbo");
            //           });

        }
    }
}
