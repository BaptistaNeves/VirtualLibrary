using System;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Services
{
    public interface IAutorService: IDisposable
    {
        Task Adicionar(Autor autor);
        Task Atualizar(Autor autor);
        Task Remover(Autor autor);
    }
}