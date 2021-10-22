using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Data.Mappings;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Data.Context
{
    public class AppDbContext : IdentityDbContext<Usuario, Role, Guid,
                                IdentityUserClaim<Guid>, UserRole, IdentityUserLogin<Guid>, IdentityRoleClaim<Guid>,
                                IdentityUserToken<Guid>>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){ }

         public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Autor> Autores { get; set; }
        public DbSet<Editora> Editoras { get; set; }
        public DbSet<Fornecedor> Fornecedores { get; set; }
        public DbSet<Livro> Livros { get; set; }
        public DbSet<Aluguer> Alugueres { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aplicar os mapeamentos associados as entidades.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

            base.OnModelCreating(modelBuilder);

            modelBuilder.MapUsuarioRelations();
            
            modelBuilder.Entity<Role>()
                        .HasMany(r => r.UserRoles)
                        .WithOne(ur => ur.Role)
                        .HasForeignKey(ur => ur.RoleId)
                        .IsRequired();
            
             modelBuilder.Entity<Usuario>()
                        .HasMany(u => u.UserRoles)
                        .WithOne(ur => ur.Usuario)
                        .HasForeignKey(ur => ur.UserId)
                        .IsRequired();

            // foreach (var relationship in modelBuilder.Model.GetEntityTypes()
            //         .SelectMany(e => e.GetForeignKeys()))
            // {
            //     relationship.DeleteBehavior = DeleteBehavior.Restrict;
            // }

        }

    }
}