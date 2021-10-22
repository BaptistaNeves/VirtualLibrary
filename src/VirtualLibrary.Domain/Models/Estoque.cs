using System;

namespace VirtualLibrary.Domain.Models
{
    public class Estoque : Entity
    {
        public Guid LivroId { get; set; }
        public decimal Quantidade { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }
    }
}