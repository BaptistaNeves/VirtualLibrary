using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq.Expressions;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : class
    {
        Task Adicionar(TEntity entity);
        Task Atualizar(TEntity entity);
        Task Remover(TEntity entity);
        Task<TEntity> ObterPorId(Guid id);
        Task<IEnumerable<TEntity>> ObterTodos();
        Task<IEnumerable<TEntity>> Buscar(Expression<Func<TEntity, bool>> predicated);
        Task<int> SaveChangesAsync();
        
    }
}