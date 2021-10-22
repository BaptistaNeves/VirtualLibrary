using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Mappings
{
    public class ClienteMapping : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
             builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            
            builder.Property(c => c.Email)
                    .IsRequired()
                    .HasColumnType("varchar(255)");
            
            builder.Property(c => c.Telefone)
                    .IsRequired()
                    .HasColumnType("varchar(15)");

            builder.Property(c => c.Endereco)
                    .IsRequired()
                    .HasColumnType("varchar(300)");

            builder.Property(c => c.TipoDocumento)
                    .IsRequired()
                    .HasColumnType("varchar(50)");

            builder.Property(c => c.NumeroDocumento)
                    .IsRequired()
                    .HasColumnType("varchar(255)");

            builder.Property(c => c.UsuarioId)
                   .IsRequired(false);

            builder.Property(c => c.DataNascimento)
                    .IsRequired()
                    .HasColumnType("datetime2");

            builder.Property(c => c.DataCadastro)
                    .HasColumnType("datetime2");

            builder.Property(c => c.DataAtualizacao)
                    .HasColumnType("datetime2");

            builder.HasMany(c => c.Alugueres)
                    .WithOne(a => a.Cliente)
                    .HasForeignKey(a => a.ClienteId)
                    .IsRequired();

            builder.ToTable("Clientes");
        }
    }
}