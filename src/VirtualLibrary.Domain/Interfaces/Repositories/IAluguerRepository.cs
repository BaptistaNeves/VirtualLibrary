using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using VirtualLibrary.Domain.Models;

namespace VirtualLibrary.Domain.Interfaces.Repositories
{
    public interface IAluguerRepository : IRepository<Aluguer>
    {
        Task<Aluguer> ObterAluguerDetalhesPorId(Guid id);
        Task<IEnumerable<Aluguer>> ObterAlugueresDetalhes();
        Task<IEnumerable<Aluguer>> ObterAluguresEmCurso();
        Task<IEnumerable<Aluguer>> ObterAluguresVencidos();
        Task<IEnumerable<Aluguer>> ObterAluguresCompletados();

        
    }
}