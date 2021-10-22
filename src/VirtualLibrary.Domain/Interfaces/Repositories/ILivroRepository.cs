using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Repositories
{
    public interface ILivroRepository : IRepository<Livro>
    {
        Task<Livro> ObterDetalhesDeLivroPorId(Guid id);
        Task<IEnumerable<Livro>> ObterDetalhesDeLivros();
        Task<Livro> ObterLivroDisponivelPorId(Guid id);
        Task<IEnumerable<Livro>> ObterLivrosDisponiveis();
        Task<Livro> ObterLivroAlugueresPorId(Guid id);
        Task<IEnumerable<Livro>> ObterLivroPorCategoriaId(Guid id);
        
    }
}