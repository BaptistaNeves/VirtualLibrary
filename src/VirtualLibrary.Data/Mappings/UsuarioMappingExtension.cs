using System;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Mappings
{
    public static class UsuarioMappingExtension
    {
        public static ModelBuilder MapUsuarioRelations(this ModelBuilder modelBuilder)
        {
            
            modelBuilder.Entity<Usuario>()
                        .HasMany(u => u.Categorias)
                        .WithOne(c => c.Usuario)
                        .HasForeignKey(c => c.UsuarioId)
                        .OnDelete(DeleteBehavior.SetNull);
            
            modelBuilder.Entity<Usuario>()
                        .HasMany(u => u.Autores)
                        .WithOne(c => c.Usuario)
                        .HasForeignKey(c => c.UsuarioId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Usuario>()
                        .HasMany(u => u.Editoras)
                        .WithOne(c => c.Usuario)
                        .HasForeignKey(c => c.UsuarioId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Usuario>()
                        .HasMany(u => u.Fornecedores)
                        .WithOne(c => c.Usuario)
                        .HasForeignKey(c => c.UsuarioId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Usuario>()
                        .HasMany(u => u.Livros)
                        .WithOne(c => c.Usuario)
                        .HasForeignKey(c => c.UsuarioId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Usuario>()
                        .HasMany(u => u.Clientes)
                        .WithOne(c => c.Usuario)
                        .HasForeignKey(c => c.UsuarioId)
                        .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Usuario>()
                        .HasMany(u => u.Alugueres)
                        .WithOne(c => c.Usuario)
                        .HasForeignKey(c => c.UsuarioId)
                        .OnDelete(DeleteBehavior.SetNull);
                        
           return modelBuilder;
        }
    }
}