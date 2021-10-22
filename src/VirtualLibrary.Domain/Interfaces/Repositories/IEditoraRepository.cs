using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Repositories
{
    public interface IEditoraRepository : IRepository<Editora>
    {
        Task<Editora> ObterEditoraPorNome(string nome);
        Task<IEnumerable<Editora>> ObterEditoras();
        Task<Editora> ObterEditoraPorId(Guid id);
        Task<Editora> ObterEditoraLivrosPorNome(string nome);
        Task<Editora> ObterEditoraLivrosPorId(Guid id);
        Task<IEnumerable<Editora>> ObterEditorasLivros();
    }
}