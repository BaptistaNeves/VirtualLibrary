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
    public class CategoriaRepository : Repository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(AppDbContext db) : base(db){}

        public async Task<Categoria> ObterCategoriaLivrosPorDescricao(string descricao)
        {
            return await _db.Categorias.AsNoTracking()
                                .Include(l => l.Livros)
                                .Select(c => new Categoria 
                                {
                                    Id = c.Id,
                                    Descricao = c.Descricao,
                                    DataCadastro = c.DataCadastro,
                                    DataAtualizacao = c.DataAtualizacao,
                                    Livros = c.Livros.Select(l => new Livro {
                                        Titulo = l.Titulo,
                                        Descricao = l.Descricao,
                                        Imagem = l.Imagem,
                                        Estado = l.Estado
                                    })
                                })
                                .FirstOrDefaultAsync(c => c.Descricao == descricao);
        }

        public async Task<Categoria> ObterCategoriaLivrosPorId(Guid id)
        {
             return await _db.Categorias.AsNoTracking()
                                .Include(l => l.Livros)
                                .Select(c => new Categoria 
                                {
                                    Id = c.Id,
                                    Descricao = c.Descricao,
                                    DataCadastro = c.DataCadastro,
                                    DataAtualizacao = c.DataAtualizacao,
                                    Livros = c.Livros.Select(l => new Livro {
                                        Titulo = l.Titulo,
                                        Descricao = l.Descricao,
                                        Imagem = l.Imagem,
                                        Estado = l.Estado
                                    })
                                })
                                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Categoria> ObterCategoriaPorDescricao(string descricao)
        {
            return await _db.Categorias.AsNoTracking()
                            .FirstOrDefaultAsync(c => c.Descricao == descricao);
        }

        public async Task<IEnumerable<Categoria>> ObterCategorias()
        {
            return await _db.Categorias.AsNoTracking()
                         .Include(c => c.Usuario)
                         .Select(c => new Categoria {
                            Id = c.Id,
                            Descricao = c.Descricao,
                            DataCadastro = c.DataCadastro,
                            DataAtualizacao = c.DataAtualizacao,
                            Usuario = new Usuario {
                                UserName = c.Usuario.UserName
                            }
                         })
                         .ToListAsync();
        }

        public async Task<Categoria> ObterCategoriaPorId(Guid id)
        {
            return await _db.Categorias.AsNoTracking()
                         .Include(c => c.Usuario)
                         .Select(c => new Categoria {
                            Id = c.Id,
                            Descricao = c.Descricao,
                            DataCadastro = c.DataCadastro,
                            DataAtualizacao = c.DataAtualizacao,
                            Usuario = new Usuario {
                                UserName = c.Usuario.UserName
                            }
                         })
                         .FirstOrDefaultAsync(c =>c.Id == id);
        }

        public async Task<IEnumerable<Categoria>> ObterCategoriasLivros(string descricao)
        {
            return await _db.Categorias.AsNoTracking()
                                .Include(l => l.Livros)
                                .Select(c => new Categoria 
                                {
                                    Id = c.Id,
                                    Descricao = c.Descricao,
                                    DataCadastro = c.DataCadastro,
                                    DataAtualizacao = c.DataAtualizacao,
                                    Livros = c.Livros.Select(l => new Livro {
                                        Titulo = l.Titulo,
                                        Descricao = l.Descricao,
                                        Imagem = l.Imagem,
                                        Estado = l.Estado
                                    })
                                }).ToListAsync();
        }
    }
}