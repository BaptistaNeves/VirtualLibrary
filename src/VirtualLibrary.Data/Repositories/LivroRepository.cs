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
    public class LivroRepository : Repository<Livro>, ILivroRepository
    {
        public LivroRepository(AppDbContext db) : base(db) {}

        public async Task<Livro> ObterDetalhesDeLivroPorId(Guid id)
        {
            return await _db.Livros.AsNoTracking()
                        .Where(l => l.Id == id)
                        .Include(l => l.Categoria)
                        .Include(l => l.Autor)
                        .Include(l => l.Editora)
                        .Include(l => l.Fornecedor)
                        .Include(l => l.Usuario)
                        .Select(l => new Livro
                        {
                            Titulo = l.Titulo,
                            Descricao = l.Descricao,
                            Imagem = l.Imagem,
                            Estado = l.Estado,
                            DataCadastro = l.DataCadastro,
                            DataAtualizacao = l.DataAtualizacao,
                            Fornecedor = new Fornecedor {Nome = l.Fornecedor.Nome},
                            Categoria = new Categoria {Descricao = l.Categoria.Descricao},
                            Autor = new Autor {Nome = l.Autor.Nome},
                            Editora = new Editora {Nome = l.Editora.Nome},
                            Usuario = new Usuario {UserName = l.Usuario.UserName}
                        })
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Livro>> ObterDetalhesDeLivros()
        {
            return await _db.Livros.AsNoTracking()
                        .Include(l => l.Categoria)
                        .Include(l => l.Autor)
                        .Include(l => l.Editora)
                        .Include(l => l.Fornecedor)
                        .Include(l => l.Usuario)
                        .Select(l => new Livro
                        {
                            Titulo = l.Titulo,
                            Descricao = l.Descricao,
                            Imagem = l.Imagem,
                            Estado = l.Estado,
                            DataCadastro = l.DataCadastro,
                            DataAtualizacao = l.DataAtualizacao,
                            Fornecedor = new Fornecedor {Nome = l.Fornecedor.Nome},
                            Categoria = new Categoria {Descricao = l.Categoria.Descricao},
                            Autor = new Autor {Nome = l.Autor.Nome},
                            Editora = new Editora {Nome = l.Editora.Nome},
                            Usuario = new Usuario {UserName = l.Usuario.UserName}
                        })
                        .ToListAsync();
        }

        public async Task<Livro> ObterLivroAlugueresPorId(Guid id)
        {
            return await _db.Livros.AsNoTracking()
                        .Include(l => l.Alugueres)
                        .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<Livro> ObterLivroDisponivelPorId(Guid id)
        {
            return await _db.Livros.AsNoTracking()
                        .Where(l => l.Id == l.Id && l.Estado == true)
                        .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Livro>> ObterLivrosDisponiveis()
        {
            return await _db.Livros.AsNoTracking()
                        .Where(l => l.Estado == true)
                        .ToListAsync();
        }

        public override async Task<Livro> ObterPorId(Guid id)
        {
            return await _db.Livros.AsNoTracking()
                        .FirstOrDefaultAsync(l => l.Id == id);
        }

        public async Task<IEnumerable<Livro>> ObterLivroPorCategoriaId(Guid categoriaId)
        {
            return await _db.Livros.AsNoTracking()
                            .Where(l => l.CategoriaId == categoriaId)
                            .Include(l => l.Categoria)
                            .Include(l => l.Autor)
                            .Include(l => l.Editora)
                            .Include(l => l.Fornecedor)
                            .Include(l => l.Usuario)
                            .Select(l => new Livro
                            {
                                Titulo = l.Titulo,
                                Descricao = l.Descricao,
                                Imagem = l.Imagem,
                                Estado = l.Estado,
                                DataCadastro = l.DataCadastro,
                                DataAtualizacao = l.DataAtualizacao,
                                Fornecedor = new Fornecedor {Nome = l.Fornecedor.Nome},
                                Categoria = new Categoria {Descricao = l.Categoria.Descricao},
                                Autor = new Autor {Nome = l.Autor.Nome},
                                Editora = new Editora {Nome = l.Editora.Nome},
                                Usuario = new Usuario {UserName = l.Usuario.UserName}
                            })
                            .ToListAsync();
        }
    }
}