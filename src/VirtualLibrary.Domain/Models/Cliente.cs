using System;
using System.Collections.Generic;

namespace VirtualLibrary.Domain.Models
{
    public class Cliente : Entity
    {
        public Guid? UsuarioId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string Telefone { get; set; }
        public string Endereco { get; set; }
        public string TipoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime DataNascimento { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime DataAtualizacao { get; set; }

        public Usuario Usuario { get; set; }
        /*Ef Relation*/
        public IEnumerable<Aluguer> Alugueres { get; set; }
    }
}