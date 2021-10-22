using System;

namespace VirtualLibrary.Domain.Models
{
    public class Aluguer : Entity
    {
        public Guid?
         UsuarioId { get; set; }
        public Guid ClienteId { get; set; }
        public Guid LivroId { get; set; }

        public decimal Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal PrecoTotal { get; set; }
        public bool EmCurso { get; set; }
        public bool Vencido { get; set; }
        public bool Completo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

        /*EF Relations*/
        public Cliente Cliente { get; set; }
        public Livro Livro { get; set; }
        public Usuario Usuario { get; set; }
    }
}