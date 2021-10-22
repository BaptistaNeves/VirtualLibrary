using System;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Services
{
    public interface IEditoraService : IDisposable
    {
        Task Adicionar(Editora editora);
        Task Atualizar(Editora editora);
        Task Remover(Editora editora);
    }
}