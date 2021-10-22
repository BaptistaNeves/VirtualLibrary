using System;
using System.Collections.Generic;

namespace VirtualLibrary.Domain.Models
{
    public class Livro : Entity
    {
        public Guid? UsuarioId { get; set; }
        public Guid CategoriaId { get; set; }
        public Guid AutorId { get; set; }
        public Guid EditoraId { get; set; }
        public Guid FornecedorId { get; set; }

        public string Titulo { get; set; }
        public string Imagem { get; set; }
        public string Descricao { get; set; }
        public bool Estado { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

        /*EF Core - Relations*/
        public Usuario Usuario { get; set; }
        public Categoria Categoria { get; set; }
        public Autor Autor { get; set; }
        public Editora Editora { get; set; }
        public Fornecedor Fornecedor { get; set; }

        public IEnumerable<Aluguer> Alugueres { get; set; }
    }
}