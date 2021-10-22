using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VirtualLibrary.Domain.Interfaces.Repositories;
using VirtualLibrary.Domain.Models;
using VirtualLibrary.Data.Context;
using System.Linq;

namespace VirtualLibrary.Data.Repositories
{
    public class FornecedorRepository : Repository<Fornecedor>, IFornecedorRepository
    {
        public FornecedorRepository(AppDbContext db) : base(db){}

        public async Task<IEnumerable<Fornecedor>> ObterFornecedoresLivros()
        {
            return await _db.Fornecedores.AsNoTracking()
                        .Include(f => f.Livros)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Fornecedor>> ObterFornecedores()
        {
             return await _db.Fornecedores.AsNoTracking()
                        .Include(f => f.Usuario)
                        .Select(f => new Fornecedor
                        {
                            Id = f.Id,
                            email = f.email,
                            Telefone = f.Telefone,
                            Nome = f.Nome,
                            Endereco = f.Endereco,
                            TipoDocumento = f.TipoDocumento,
                            NumeroDocumento = f.NumeroDocumento,
                            DataNascimento = f.DataNascimento,
                            DataCadastro = f.DataCadastro,
                            DataAtualizacao = f.DataAtualizacao,
                            Usuario = new Usuario {
                                UserName = f.Usuario.UserName
                            }
                        })
                        .ToListAsync();
        }

        public async Task<Fornecedor> ObterFornecedorPorId(Guid id)
        {
             return await _db.Fornecedores.AsNoTracking()
                        .Include(f => f.Usuario)
                        .Select(f => new Fornecedor
                        {
                            Id = f.Id,
                            Nome = f.Nome,
                            Endereco = f.Endereco,
                            TipoDocumento = f.TipoDocumento,
                            NumeroDocumento = f.NumeroDocumento,
                            DataNascimento = f.DataNascimento,
                            DataCadastro = f.DataCadastro,
                            DataAtualizacao = f.DataAtualizacao,
                            Usuario = new Usuario {
                                UserName = f.Usuario.UserName
                            }
                        })
                        .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorLivrosPorId(Guid id)
        {
            return await _db.Fornecedores.AsNoTracking()
                        .Include(f => f.Livros)
                        .FirstOrDefaultAsync(f => f.Id == id);
        }

        public async Task<Fornecedor> ObterFornecedorLivrosPorNome(string nome)
        {
            return await _db.Fornecedores.AsNoTracking()
                        .Include(f => f.Livros)
                        .FirstOrDefaultAsync(f => f.Nome == nome);
        }

        public async Task<Fornecedor> ObterFornecedorPorNome(string nome)
        {
            return await _db.Fornecedores.AsNoTracking()
                        .FirstOrDefaultAsync(f => f.Nome == nome);
        }

        public override async Task<Fornecedor> ObterPorId(Guid id) 
        {
            return await _db.Fornecedores
                            .AsNoTracking()
                            .FirstOrDefaultAsync(f => f.Id == id);
        }
    }
}