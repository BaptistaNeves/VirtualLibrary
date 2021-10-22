using System;
using System.Collections.Generic;

namespace VirtualLibrary.Domain.Models
{
    public class Autor : Entity
    {
        public Guid? UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Nacionalidade { get; set; }
        public string Descricao { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

        /*Ef Relation 1:N*/
        public IEnumerable<Livro> Livros { get; set; }
        public Usuario Usuario { get; set; }
    }
}