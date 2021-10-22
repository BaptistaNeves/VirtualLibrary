using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Models;
using VirtualLibrary.Data.Context;

namespace VirtualLibrary.Data.Repositories
{
    public class EditoraRepository : Repository<Editora>, IEditoraRepository
    {
        public EditoraRepository(AppDbContext db) : base(db){}

        public async Task<Editora> ObterEditoraLivrosPorId(Guid id)
        {
            return await _db.Editoras.AsNoTracking()
                        .Include(l => l.Livros)
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Editora> ObterEditoraLivrosPorNome(string nome)
        {
            return await _db.Editoras.AsNoTracking()
                        .Include(l => l.Livros)
                        .FirstOrDefaultAsync(e => e.Nome == nome);
        }

        public async Task<Editora> ObterEditoraPorNome(string nome)
        {
            return await _db.Editoras.AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Nome == nome);
        }

        public override async Task<Editora> ObterPorId(Guid id)
        {
            return await _db.Editoras.AsNoTracking()
                        .FirstOrDefaultAsync(e => e.Id == id);
        }
        public async Task<IEnumerable<Editora>> ObterEditoras()
        {
            return await _db.Editoras.AsNoTracking()
                        .Include(e => e.Usuario)
                        .Select(e => new Editora 
                        {
                            Id = e.Id,
                            Nome = e.Nome,
                            Cidade = e.Cidade,
                            Pais = e.Pais,
                            Descricao = e.Descricao,
                            DataCadastro = e.DataCadastro,
                            DataAtualizacao = e.DataAtualizacao,
                            Usuario = new Usuario {
                                UserName = e.Usuario.UserName
                            }
                        })
                        .ToListAsync();
        }

         public async Task<Editora> ObterEditoraPorId(Guid id)
        {
            return await _db.Editoras.AsNoTracking()
                        .Include(e => e.Usuario)
                        .Select(e => new Editora 
                        {
                            Id = e.Id,
                            Nome = e.Nome,
                            Cidade = e.Cidade,
                            Pais = e.Pais,
                            Descricao = e.Descricao,
                            DataCadastro = e.DataCadastro,
                            DataAtualizacao = e.DataAtualizacao,
                            Usuario = new Usuario {
                                UserName = e.Usuario.UserName
                            }
                        })
                        .FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<IEnumerable<Editora>> ObterEditorasLivros()
        {
            return await _db.Editoras.AsNoTracking()
                        .Include(e =>e.Livros)
                        .ToListAsync();
        }
    }
}