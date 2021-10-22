using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Repositories
{
    public interface ICategoriaRepository : IRepository<Categoria>
    {
        Task<Categoria> ObterCategoriaPorDescricao(string descricao);
        Task<IEnumerable<Categoria>> ObterCategorias();
        Task<Categoria> ObterCategoriaPorId(Guid id);
        Task<Categoria> ObterCategoriaLivrosPorDescricao(string descricao);
        Task<Categoria> ObterCategoriaLivrosPorId(Guid id);
        Task<IEnumerable<Categoria>> ObterCategoriasLivros(string descricao);

    }
}