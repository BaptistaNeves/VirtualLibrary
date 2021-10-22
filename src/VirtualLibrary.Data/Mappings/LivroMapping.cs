using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Mappings
{
    public class LivroMapping : IEntityTypeConfiguration<Livro>
    {
        public void Configure(EntityTypeBuilder<Livro> builder)
        {
            builder.HasKey(l => l.Id);

            builder.Property(l => l.Titulo)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            
            builder.Property(l => l.Imagem)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

            builder.Property(l => l.Descricao)
                    .IsRequired()
                    .HasColumnType("varchar(1000)");

            builder.Property(l => l.Estado)
                    .HasColumnType("bit");

            builder.Property(l => l.UsuarioId)
                   .IsRequired(false);

            builder.Property(l => l.DataCadastro)
                    .HasColumnType("datetime2");

            builder.Property(l => l.DataAtualizacao)
                    .HasColumnType("datetime2");

            builder.HasMany(l => l.Alugueres)
                    .WithOne(a => a.Livro)
                    .HasForeignKey(a => a.LivroId)
                    .IsRequired();

            builder.ToTable("Livros");
        }
    }
}