using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Mappings
{
    public class EditoraMapping : IEntityTypeConfiguration<Editora>
    {
        public void Configure(EntityTypeBuilder<Editora> builder)
        {
            builder.HasKey(e => e.Id);

            builder.Property(e => e.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            
            builder.Property(e => e.Pais)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

            builder.Property(e => e.Cidade)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

            builder.Property(e => e.Descricao)
                    .HasColumnType("varchar(1000)");

            builder.Property(e => e.UsuarioId)
                    .IsRequired(false);

            builder.Property(e => e.DataCadastro)
                    .HasColumnType("datetime2");

            builder.Property(e => e.DataAtualizacao)
                    .HasColumnType("datetime2");

            builder.HasMany(e => e.Livros)
                    .WithOne(l => l.Editora)
                    .HasForeignKey(l => l.EditoraId)
                    .IsRequired();

            builder.ToTable("Editoras");
        }
    }
}