using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Mappings
{
    public class AluguerMapping : IEntityTypeConfiguration<Aluguer>
    {
        public void Configure(EntityTypeBuilder<Aluguer> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Quantidade)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

            builder.Property(a => a.PrecoUnitario)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

            builder.Property(a => a.PrecoTotal)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");

            builder.Property(a => a.EmCurso)
                    .HasColumnType("bit");

             builder.Property(a => a.Vencido)
                    .HasColumnType("bit");

             builder.Property(a => a.Completo)
                    .HasColumnType("bit");

             builder.Property(a => a.UsuarioId)
                    .IsRequired(false);

             builder.Property(a => a.DataCadastro)
                    .HasColumnType("datetime2");

             builder.Property(a => a.DataAtualizacao)
                    .HasColumnType("datetime2");

            builder.ToTable("Alugueres");
        }
    }
}