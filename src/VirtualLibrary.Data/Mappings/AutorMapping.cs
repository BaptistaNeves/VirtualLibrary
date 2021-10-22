using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Mappings
{
    public class AutorMapping : IEntityTypeConfiguration<Autor>
    {
        public void Configure(EntityTypeBuilder<Autor> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

            builder.Property(a => a.Nacionalidade)
                    .IsRequired()
                    .HasColumnType("varchar(50)");
            
            builder.Property(a => a.Descricao)
                    .HasColumnType("varchar(1000)");

            builder.Property(a => a.UsuarioId)
                    .IsRequired(false);

            builder.Property(a => a.DataCadastro)
                    .HasColumnType("datetime2");

            builder.Property(a => a.DataAtualizacao)
                    .HasColumnType("datetime2");

            builder.HasMany(a => a.Livros)
                    .WithOne(l => l.Autor)
                    .HasForeignKey(l => l.AutorId)
                    .IsRequired();

            builder.ToTable("Autores");
        }
    }
}