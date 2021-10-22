using System;
using System.Collections.Generic;

namespace VirtualLibrary.Domain.Models
{
    public class Editora : Entity
    {
        public Guid? UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Pais { get; set; }
        public string Cidade { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
        public Usuario Usuario { get; set; }
        /*EF Relation*/
        public IEnumerable<Livro> Livros { get; set; }
    }
}