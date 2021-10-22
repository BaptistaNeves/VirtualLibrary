using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Repositories
{
    public interface IAutorRepository : IRepository<Autor>
    {
        Task<Autor> ObterAutorPorNome(string nome);
        Task<IEnumerable<Autor>> ObterAutores();
        Task<Autor> ObterAutorPorId(Guid id);
        Task<Autor> ObterAutorLivrosPorNome(string nome);
        Task<Autor> ObterAutorLivrosPorId(Guid id);
        Task<IEnumerable<Autor>> ObterAutoresLivros();
    }
}