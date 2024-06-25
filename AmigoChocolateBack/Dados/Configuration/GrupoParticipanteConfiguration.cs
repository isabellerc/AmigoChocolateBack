using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AmigoChocolateBack.Models.Configurations
{
    public class GrupoParticipante2Configuration : IEntityTypeConfiguration<GrupoParticipante2>
    {
        public void Configure(EntityTypeBuilder<GrupoParticipante2> builder)
        {
            builder.ToTable("GrupoParticipante2"); // Especifica o nome correto da tabela

            builder.HasKey(gp => gp.IDGrupoParticipante); // Define a chave primária
            builder.Property(gp => gp.IDGrupoParticipante).ValueGeneratedNever(); // Remove o auto-incremento

            // Garante que IDGrupoParticipante será igual a IDGrupo
            builder.Property(gp => gp.IDGrupoParticipante)
                   .HasDefaultValueSql("IDGrupo");

            builder.HasIndex(gp => new { gp.IDGrupo, gp.IDParticipante }).IsUnique(); // Garante a unicidade das combinações de IDGrupo e IDParticipante

            builder.HasOne(gp => gp.Grupo)
                .WithMany(g => g.GrupoParticipantes2)
                .HasForeignKey(gp => gp.IDGrupo);

            builder.HasOne(gp => gp.Participante)
                .WithMany(p => p.GrupoParticipantes2)
                .HasForeignKey(gp => gp.IDParticipante);
        }
    }
}
