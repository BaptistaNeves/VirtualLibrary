using System;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Services
{
    public interface ILivroService : IDisposable
    {
        Task Adicionar(Livro livro);
        Task Atualizar(Livro livro);
        Task Remover(Livro livro);
    }
}