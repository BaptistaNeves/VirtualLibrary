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
    public class ClienteRepository : Repository<Cliente>, IClienteRepository
    {
        public ClienteRepository(AppDbContext db) : base(db){}

        public async Task<Cliente> ObterClienteAlugueresPorId(Guid id)
        {
            return await _db.Clientes.AsNoTracking()
                        .Include(c => c.Alugueres)
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Cliente> ObterClienteAlugueresPorNome(string nome)
        {
            return await _db.Clientes.AsNoTracking()
                        .Include(c => c.Alugueres)
                        .FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public async Task<Cliente> ObterClientePorNome(string nome)
        {
           return await _db.Clientes.AsNoTracking()
                        .FirstOrDefaultAsync(c => c.Nome == nome);
        }

        public async Task<IEnumerable<Cliente>> ObterClientesAlugueres()
        {
            return await _db.Clientes.AsNoTracking()
                        .Include(c => c.Alugueres)
                        .ToListAsync();
        }

        public async Task<IEnumerable<Cliente>> ObterClientes()
        {
            return await _db.Clientes.AsNoTracking()
                        .Include(c => c.Usuario)
                        .Select(c => new Cliente
                        {
                            Id = c.Id,
                            Nome = c.Nome,
                            Email = c.Email,
                            Telefone = c.Telefone,
                            Endereco = c.Endereco,
                            TipoDocumento = c.TipoDocumento,
                            NumeroDocumento = c.NumeroDocumento,
                            DataNascimento = c.DataNascimento,
                            DataCadastro = c.DataCadastro,
                            DataAtualizacao = c.DataAtualizacao,
                            Usuario = new Usuario {
                                UserName = c.Usuario.UserName
                            }
                        })
                        .ToListAsync();
        }

        public async Task<Cliente> ObterClientePorId(Guid id)
        {
            return await _db.Clientes.AsNoTracking()
                        .Include(c => c.Usuario)
                        .Select(c => new Cliente
                        {
                            Id = c.Id,
                            Nome = c.Nome,
                            Email = c.Email,
                            Telefone = c.Telefone,
                            Endereco = c.Endereco,
                            TipoDocumento = c.TipoDocumento,
                            NumeroDocumento = c.NumeroDocumento,
                            DataNascimento = c.DataNascimento,
                            DataCadastro = c.DataCadastro,
                            DataAtualizacao = c.DataAtualizacao,
                            Usuario = new Usuario {
                                UserName = c.Usuario.UserName
                            }
                        })
                        .FirstOrDefaultAsync(c => c.Id == id);
        }

        public override async Task<Cliente> ObterPorId(Guid id)
        {
            return await _db.Clientes.AsNoTracking()
                                    .FirstOrDefaultAsync(c => c.Id == id);
        }

    }
}