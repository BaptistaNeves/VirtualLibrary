using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace VirtualLibrary.Domain.Models
{
    public class Usuario : IdentityUser<Guid>
    {
        public string Endereco { get; set; }
        public IEnumerable<Categoria> Categorias { get; set; }
        public IEnumerable<Autor> Autores { get; set; }
        public IEnumerable<Editora> Editoras { get; set; }
        public IEnumerable<Fornecedor> Fornecedores { get; set; }
        public IEnumerable<Cliente> Clientes { get; set; }
        public IEnumerable<Livro> Livros { get; set; }
        public IEnumerable<Aluguer> Alugueres { get; set; }
        public IEnumerable<UserRole> UserRoles { get; set; }

    }
}