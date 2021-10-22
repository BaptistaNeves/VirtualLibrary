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
    public class AutorRepository : Repository<Autor>, IAutorRepository
    {
        public AutorRepository(AppDbContext db) : base(db){ }

        public async Task<Autor> ObterAutorLivrosPorId(Guid id)
        {
            return await _db.Autores.AsNoTracking()
                        .Include(l => l.Livros)
                        .Select(a => new Autor 
                        {
                            Id = a.Id,
                            Nome = a.Nome,
                            Nacionalidade = a.Nacionalidade,
                            Descricao = a.Descricao,
                            DataCadastro = a.DataCadastro,
                            DataAtualizacao = a.DataAtualizacao,
                            Livros = a.Livros.Select(l => new Livro 
                            {
                                Id = l.Id,
                                Titulo = l.Titulo,
                                Descricao = l.Descricao,
                                Imagem = l.Imagem
                            })}).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Autor> ObterAutorPorNome(string nome)
        {
            return await _db.Autores.AsNoTracking()
                        .FirstOrDefaultAsync(a => a.Nome == nome);
        }

        public async Task<Autor> ObterAutorLivrosPorNome(string nome)
        {
            return await _db.Autores.AsNoTracking()
                        .Include(l => l.Livros)
                        .Select(a => new Autor 
                        {
                            Id = a.Id,
                            Nome = a.Nome,
                            Nacionalidade = a.Nacionalidade,
                            Descricao = a.Descricao,
                            DataCadastro = a.DataCadastro,
                            DataAtualizacao = a.DataAtualizacao,
                            Livros = a.Livros.Select(l => new Livro 
                            {
                                Id = l.Id,
                                Titulo = l.Titulo,
                                Descricao = l.Descricao,
                                Imagem = l.Imagem
                            })}).FirstOrDefaultAsync(a => a.Nome == nome);
        }

        public async Task<IEnumerable<Autor>> ObterAutoresLivros()
        {
            return await _db.Autores.AsNoTracking()
                        .Include(l => l.Livros)
                        .Select(a => new Autor 
                        {
                            Id = a.Id,
                            Nome = a.Nome,
                            Nacionalidade = a.Nacionalidade,
                            Descricao = a.Descricao,
                            DataCadastro = a.DataCadastro,
                            DataAtualizacao = a.DataAtualizacao,
                            Livros = a.Livros.Select(l => new Livro 
                            {
                                Id = l.Id,
                                Titulo = l.Titulo,
                                Descricao = l.Descricao,
                                Imagem = l.Imagem
                            })}).ToListAsync();
        }

        public async Task<IEnumerable<Autor>> ObterAutores()
        {
            return await _db.Autores.AsNoTracking()
                            .Include(a => a.Usuario)
                            .Select(a => new Autor {
                                Id = a.Id,
                                Nome = a.Nome,
                                Nacionalidade = a.Nacionalidade,
                                Descricao = a.Descricao,
                                Usuario = new Usuario {
                                    UserName = a.Usuario.UserName
                                }
                            })
                            .ToListAsync();
        }

        public async Task<Autor> ObterAutorPorId(Guid id)
        {
            return await _db.Autores.AsNoTracking()
                            .Include(a => a.Usuario)
                            .Select(a => new Autor {
                                Id = a.Id,
                                Nome = a.Nome,
                                Nacionalidade = a.Nacionalidade,
                                Descricao = a.Descricao,
                                Usuario = new Usuario {
                                    UserName = a.Usuario.UserName
                                }
                            })
                            .FirstOrDefaultAsync(a => a.Id == id);
        }

    }
}