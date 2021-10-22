using System;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Services
{
    public interface IFornecedorService : IDisposable
    {
        Task Adicionar(Fornecedor fornecedor);
        Task Atualizar(Fornecedor fornecedor);
        Task Remover(Fornecedor fornecedor);
    }
}