using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Repositories
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorPorNome(string nome);
        Task<Fornecedor> ObterFornecedorLivrosPorNome(string nome);
        Task<Fornecedor> ObterFornecedorLivrosPorId(Guid id);
        Task<IEnumerable<Fornecedor>> ObterFornecedoresLivros();
        Task<IEnumerable<Fornecedor>> ObterFornecedores();
        Task<Fornecedor> ObterFornecedorPorId(Guid id);
        
    }
}