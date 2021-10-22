using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Repositories
{
    public interface IClienteRepository : IRepository<Cliente>
    {
        Task<Cliente> ObterClientePorNome(string nome);
        Task<Cliente> ObterClienteAlugueresPorNome(string nome);
        Task<Cliente> ObterClienteAlugueresPorId(Guid id);
        Task<IEnumerable<Cliente>> ObterClientesAlugueres();
        Task<IEnumerable<Cliente>> ObterClientes();
        Task<Cliente> ObterClientePorId(Guid id);
        
    }
}