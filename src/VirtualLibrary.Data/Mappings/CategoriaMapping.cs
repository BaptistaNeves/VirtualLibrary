using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Mappings
{
    public class CategoriaMapping : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Descricao)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

            builder.Property(c => c.UsuarioId)
                    .IsRequired(false);

            builder.Property(c => c.DataCadastro)
                    .HasColumnType("datetime2");

            builder.Property(c => c.DataAtualizacao)
                    .HasColumnType("datetime2");

            builder.HasMany(c => c.Livros)
                    .WithOne(l => l.Categoria)
                    .HasForeignKey(l => l.CategoriaId)
                    .IsRequired();

            builder.ToTable("Categorias");
        }
    }
}