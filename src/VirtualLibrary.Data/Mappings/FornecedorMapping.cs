using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Mappings
{
    public class FornecedorMapping : IEntityTypeConfiguration<Fornecedor>
    {
        public void Configure(EntityTypeBuilder<Fornecedor> builder)
        {
            builder.HasKey(f => f.Id);

            builder.Property(f => f.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            
            builder.Property(f => f.email)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            
            builder.Property(f => f.Telefone)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

            builder.Property(f => f.Endereco)
                    .IsRequired()
                    .HasColumnType("varchar(300)");

            builder.Property(f => f.TipoDocumento)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

            builder.Property(f => f.NumeroDocumento)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

            builder.Property(f => f.UsuarioId)
                    .IsRequired(false);

            builder.Property(f => f.DataNascimento)
                    .IsRequired()
                    .HasColumnType("datetime2");

            builder.Property(f => f.DataCadastro)
                    .HasColumnType("datetime2");

            builder.Property(f => f.DataAtualizacao)
                    .HasColumnType("datetime2");

            builder.HasMany(f => f.Livros)
                    .WithOne(l => l.Fornecedor)
                    .HasForeignKey(l => l.FornecedorId)
                    .IsRequired();

            builder.ToTable("Fornecedores");
        }
    }
}